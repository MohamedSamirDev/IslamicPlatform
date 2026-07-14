using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.Auth;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            return ApiResponse<AuthResponseDto>.Fail("Email already exists");

        var user = new ApplicationUser
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.Email,
            Country = dto.Country,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return ApiResponse<AuthResponseDto>.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

        // ✅ FIX 1: إضافة Role افتراضي للمستخدم الجديد
        await _userManager.AddToRoleAsync(user, "User");

        // ✅ FIX 2: حذف التوكنات المنتهية قبل إضافة جديدة
        await _unitOfWork.RefreshTokens.DeleteExpiredTokensAsync(user.Id);

        var accessToken = await GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        await _unitOfWork.RefreshTokens.AddAsync(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        });

        await _unitOfWork.SaveChangesAsync();

        return ApiResponse<AuthResponseDto>.Ok(new AuthResponseDto
        {
            AccessToken = accessToken.Token,
            AccessTokenExpiry = accessToken.Expiry,
            RefreshToken = refreshToken,
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email
        });
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return ApiResponse<AuthResponseDto>.Fail("Invalid email or password");

        // ✅ FIX 2: حذف التوكنات المنتهية قبل إضافة جديدة
        await _unitOfWork.RefreshTokens.DeleteExpiredTokensAsync(user.Id);

        var accessToken = await GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        await _unitOfWork.RefreshTokens.AddAsync(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            DeviceName = dto.DeviceName,
            CreatedAt = DateTime.UtcNow
        });

        await _unitOfWork.SaveChangesAsync();

        return ApiResponse<AuthResponseDto>.Ok(new AuthResponseDto
        {
            AccessToken = accessToken.Token,
            AccessTokenExpiry = accessToken.Expiry,
            RefreshToken = refreshToken,
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email
        });
    }

    public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken)
    {
        var token = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);

        if (token == null || token.IsRevoked || token.IsUsed || token.ExpiresAt < DateTime.UtcNow)
            return ApiResponse<AuthResponseDto>.Fail("Invalid or expired refresh token");

       
        var user = await _userManager.FindByIdAsync(token.UserId);
        if (user == null)
            return ApiResponse<AuthResponseDto>.Fail("User not found");

        token.IsUsed = true;

        var newRefreshToken = GenerateRefreshToken();
        token.ReplacedByToken = newRefreshToken;

        await _unitOfWork.RefreshTokens.AddAsync(new RefreshToken
        {
            Token = newRefreshToken,
            UserId = token.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            DeviceName = token.DeviceName,
            CreatedAt = DateTime.UtcNow
        });

        await _unitOfWork.RefreshTokens.UpdateAsync(token);
        await _unitOfWork.SaveChangesAsync();

    
        var accessToken = await GenerateAccessToken(user);

        return ApiResponse<AuthResponseDto>.Ok(new AuthResponseDto
        {
            AccessToken = accessToken.Token,
            AccessTokenExpiry = accessToken.Expiry,
            RefreshToken = newRefreshToken,
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email
        });
    }

    public async Task<ApiResponse<bool>> LogoutAsync(string refreshToken)
    {
        var token = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);
        if (token == null) return ApiResponse<bool>.Fail("Token not found");

        token.IsRevoked = true;
        await _unitOfWork.RefreshTokens.UpdateAsync(token);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<bool>> LogoutAllDevicesAsync(string userId)
    {
        await _unitOfWork.RefreshTokens.RevokeAllUserTokensAsync(userId);
        await _unitOfWork.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }


    private async Task<(string Token, DateTime Expiry)> GenerateAccessToken(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        var expiry = DateTime.UtcNow.AddMinutes(15);

    
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.FullName)
        };

       
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiry);
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}