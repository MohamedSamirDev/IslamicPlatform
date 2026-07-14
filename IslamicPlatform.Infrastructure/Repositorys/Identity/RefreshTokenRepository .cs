using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Interfaces.Repositories.Identity;
using IslamicPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Repositorys.Identity
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        => _context = context;

        public async Task AddAsync(RefreshToken token)
        => await
            _context.RefreshTokens.AddAsync(token);

        public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserAsync(string userId)
        => await
            _context.RefreshTokens.Where(s => s.UserId == userId && !s.IsRevoked && !s.IsUsed)
            .ToListAsync();

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        =>await 
            _context.RefreshTokens
            .Include(s=>s.User)
            .FirstOrDefaultAsync(s => s.Token == token);

        public async Task RevokeAllUserTokensAsync(string userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(s => s.UserId == userId && !s.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
                token.IsRevoked = true;
        }
                  
        public Task UpdateAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            return Task.CompletedTask;
        }
        public async Task DeleteExpiredTokensAsync(string userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(t => t.UserId == userId && (t.IsUsed || t.IsRevoked || t.ExpiresAt < DateTime.UtcNow))
                .ToListAsync();

            _context.RefreshTokens.RemoveRange(tokens);
        }
    }
}
