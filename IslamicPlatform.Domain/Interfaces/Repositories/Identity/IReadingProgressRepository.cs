using IslamicPlatform.Domain.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories.Identity
{
    public interface IReadingProgressRepository
    {
        Task<ReadingProgress?> GetByUserIdAsync(string userId);
        Task UpdateAsync(ReadingProgress progress);
        Task AddAsync(ReadingProgress progress);

    }
}
