using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.Auth;
using IslamicPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IslamicPlatform.Api.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
            => _authService = authService;

        // POST /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail("البيانات غير صحيحة"));

            var result = await _authService.RegisterAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail("البيانات غير صحيحة"));

            var result = await _authService.LoginAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST /api/auth/refresh-token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return BadRequest(ApiResponse<string>.Fail("Refresh token is required"));

            var result = await _authService.RefreshTokenAsync(refreshToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST /api/auth/logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return BadRequest(ApiResponse<string>.Fail("Refresh token is required"));

            var result = await _authService.LogoutAsync(refreshToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST /api/auth/logout-all
        [HttpPost("logout-all")]
        [Authorize]
        public async Task<IActionResult> LogoutAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _authService.LogoutAllDevicesAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
