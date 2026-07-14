using IslamicPlatform.Domain.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories.Identity
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<IEnumerable<RefreshToken>> GetActiveTokensByUserAsync(string userId);
        Task AddAsync(RefreshToken token);
        Task RevokeAllUserTokensAsync(string userId);
        Task UpdateAsync(RefreshToken token);
        Task DeleteExpiredTokensAsync(string userId);
    }
}
