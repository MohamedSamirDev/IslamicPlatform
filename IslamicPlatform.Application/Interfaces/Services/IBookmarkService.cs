using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.IdentityDTOs;
using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public interface IBookmarkService
    {
        Task<ApiResponse<IEnumerable<BookmarkDto>>> GetUserBookmarksAsync(string userId);
        Task<ApiResponse<BookmarkDto>> AddBookmarkAsync(string userId, BookmarkType type, int entityId);

         Task<ApiResponse<bool>> RemoveBookmarkAsync(string userId, int bookmarkId);
        Task<ApiResponse<bool>> IsBookmarkedAsync(string userId, BookmarkType type, int entityId);





    }
}
