using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.HadithDTOs;
using IslamicPlatform.Application.DTOs.IdentityDTOs;
using IslamicPlatform.Application.DTOs.Quran;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Enums;
using IslamicPlatform.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookmarkService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        
        public async Task<ApiResponse<BookmarkDto>> AddBookmarkAsync(string userId, BookmarkType type, int entityId)
        {
            var isAlreadyBookmarked = await _unitOfWork.Bookmarks.IsBookmarkedAsync(userId, type, entityId);
            if (isAlreadyBookmarked)
                return ApiResponse<BookmarkDto>.Fail("Already bookmarked");

            var bookmark = new Bookmark
            {
                UserId = userId,
                Type = type,
                AyahId = type == BookmarkType.Ayah ? entityId : null,
                HadithId = type == BookmarkType.Hadith ? entityId : null,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Bookmarks.AddAsync(bookmark);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<BookmarkDto>.Ok(new BookmarkDto
            {
                Id = bookmark.Id,
                Type = bookmark.Type,
                CreatedAt = bookmark.CreatedAt
            }, "Bookmarked successfully");
        }

        public async Task<ApiResponse<IEnumerable<BookmarkDto>>> GetUserBookmarksAsync(string userId)
        {

            var bookmarks = await _unitOfWork.Bookmarks.GetUserBookmarksAsync(userId);
            return ApiResponse<IEnumerable<BookmarkDto>>.Ok(bookmarks.Select(MapBookmarkToDto));
        }

        public async Task<ApiResponse<bool>> IsBookmarkedAsync(string userId, BookmarkType type, int entityId)
        {
            var result = await _unitOfWork.Bookmarks.IsBookmarkedAsync(userId, type, entityId);
            return ApiResponse<bool>.Ok(result);
        }

        public async Task<ApiResponse<bool>> RemoveBookmarkAsync(string userId, int bookmarkId)
        {
            var bookmark = await _unitOfWork.Bookmarks.GetByIdAsync(bookmarkId);
            if (bookmark == null)
                return ApiResponse<bool>.Fail("Bookmark not found");

            if (bookmark.UserId != userId)
                return ApiResponse<bool>.Fail("Unauthorized");

            await _unitOfWork.Bookmarks.DeleteAsync(bookmark);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.Ok(true, "Bookmark removed");
        }

        // ===== Private Mapping Methods =====
        private static BookmarkDto MapBookmarkToDto(Bookmark b) => new()
        {
            Id=b.Id,
            Type=b.Type,
            CreatedAt=b.CreatedAt,
            Ayah= b.Ayah==null?null:new AyahDto
            {
                Id = b.Ayah.Id,
                Number = b.Ayah.Number,
                NumberInSurah = b.Ayah.NumberInSurah,
                TextArabic = b.Ayah.TextArabic,
                JuzNumber = b.Ayah.JuzNumber,
                HizbNumber = b.Ayah.HizbNumber,
                RubNumber = b.Ayah.RubNumber

            },
            Hadith=b.Hadith==null?null:new HadithDto
            {
                Id = b.Hadith.Id,
                Number = b.Hadith.Number,
                TextArabic = b.Hadith.TextArabic,
                TextEnglish = b.Hadith.TextEnglish,
                Narrator = b.Hadith.Narrator,
                Grade = b.Hadith.Grade,
                ChapterName = b.Hadith.Chapter?.NameArabic ?? string.Empty,
                BookName = b.Hadith.Chapter?.Book?.NameArabic ?? string.Empty
            }

        };

    }
}
