using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories.Identity
{
    public interface IBookmarkRepository : IGenericRepository<Bookmark>
    {
        Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(string userId);
        Task<Bookmark?> GetUserAyahBookmarkAsync(string userId, int ayahId);
        Task<Bookmark?> GetUserHadithBookmarkAsync(string userId, int hadithId);
        Task<bool> IsBookmarkedAsync(string userId, BookmarkType type, int entityId);
    }
}
