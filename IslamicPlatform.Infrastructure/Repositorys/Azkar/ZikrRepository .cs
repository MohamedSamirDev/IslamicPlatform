using IslamicPlatform.Domain.Entites.Azkar;
using IslamicPlatform.Domain.Enums;
using IslamicPlatform.Domain.Interfaces.Repositories;
using IslamicPlatform.Domain.Interfaces.Repositories.Azkar;
using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Repositorys.Azkar
{
    public class ZikrRepository : GenericRepository<Zikr>, IZikrRepository
    {
        public ZikrRepository(ApplicationDbContext context) : base(context) { }

        public async Task AddProgressAsync(UserZikrProgress progress)
        => await _context.UserZikrProgresses.AddAsync(progress);

        public async Task<IEnumerable<UserZikrProgress>> GetAllUserProgressAsync(string userId, ZikrCategory category)
        => await
            _context.UserZikrProgresses
            .Include(s=>s.Zikr)
            .Where(a => a.UserId == userId && a.Zikr.Category==category)
            .ToListAsync();

        public async Task<IEnumerable<Zikr>> GetByCategoryAsync(ZikrCategory category)
        => await
            _dbSet.Where(a => a.Category == category)
            .ToListAsync();

        public async Task<UserZikrProgress?> GetUserProgressAsync(string userId, int zikrId)
         => await
            _context.UserZikrProgresses
            .FirstOrDefaultAsync(a => a.UserId == userId && a.ZikrId == zikrId);
        
        public async Task ResetDailyProgressAsync(string userId)//Reset all user Zikr progress for the day
        {
            var progresses = await _context.UserZikrProgresses
                  .Where(s => s.UserId == userId)
                  .ToListAsync();

            foreach(var progress in progresses)
            {
                progress.CurrentCount = 0;
                progress.IsCompleted = false;
                progress.LastResetDate = DateTime.UtcNow;
            }
        }
        
    }
}
