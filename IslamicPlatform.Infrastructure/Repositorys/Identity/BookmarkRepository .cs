using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Enums;
using IslamicPlatform.Domain.Interfaces.Repositories.Identity;
using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Repositorys.Identity
{
    public class BookmarkRepository : GenericRepository<Bookmark>, IBookmarkRepository
    {
        public BookmarkRepository(ApplicationDbContext context) : base(context) { }


        public async Task<Bookmark?> GetUserAyahBookmarkAsync(string userId, int ayahId)
        => await
            _dbSet.FirstOrDefaultAsync(s => s.UserId == userId && s.AyahId == ayahId);

        public async Task<Bookmark?> GetUserHadithBookmarkAsync(string userId, int hadithId)
        => await
            _dbSet.FirstOrDefaultAsync(s => s.UserId == userId && s.HadithId == hadithId);

        public async Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(string userId)
        => await
            _dbSet.Include(s => s.Ayah)
            .Include(s => s.Hadith)
            .Where(s => s.UserId == userId)
            .OrderByDescending(s=>s.CreatedAt)
            .ToListAsync();


        public async Task<bool> IsBookmarkedAsync(string userId, BookmarkType type, int entityId)
        =>await 
            _dbSet.AnyAsync(s=>
            s.UserId==userId &&
            s.Type==type &&
            (type== BookmarkType.Ayah ? s.AyahId == entityId : s.HadithId == entityId));

            
    }
}
