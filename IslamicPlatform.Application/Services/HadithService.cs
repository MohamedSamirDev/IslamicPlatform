using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.Common;
using IslamicPlatform.Application.DTOs.HadithDTOs;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace IslamicPlatform.Application.Services
{
    public class HadithService : IHadithService
    {
        private readonly IUnitOfWork _unitOfWork;   
        public HadithService(IUnitOfWork unitOfWork )=>_unitOfWork = unitOfWork;

        public async Task<ApiResponse<IEnumerable<HadithDto>>> GetByBookAsync(int bookId)
        {
            var hadiths = await _unitOfWork.Hadiths.GetByBookAsync(bookId);
            if (hadiths == null)
                return ApiResponse<IEnumerable<HadithDto>>.Fail("No hadiths found for the specified book.");

            return ApiResponse<IEnumerable<HadithDto>>.Ok(hadiths.Select(MapHadithToDto));
        }


        public async Task<ApiResponse<IEnumerable<HadithDto>>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return ApiResponse<IEnumerable<HadithDto>>.Fail("Keyword is required");

            var hadiths = await _unitOfWork.Hadiths.SearchAsync(keyword);

            return ApiResponse<IEnumerable<HadithDto>>.Ok(hadiths.Select(MapHadithToDto));
        }


        public async Task<ApiResponse<IEnumerable<HadithBookDto>>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Hadiths.GetAllBooksAsync();
            return ApiResponse<IEnumerable<HadithBookDto>>.Ok(books.Select(MapHadithBookToDto));
        }

        public async Task<ApiResponse<IEnumerable<HadithDto>>> GetByChapterAsync(int chapterId)
        {
            var hadiths = await _unitOfWork.Hadiths.GetByChapterAsync(chapterId);
            if (hadiths == null)
                return ApiResponse<IEnumerable<HadithDto>>.Fail("No hadiths found for the specified chapter.");
           
            return ApiResponse<IEnumerable<HadithDto>>.Ok(hadiths.Select(MapHadithToDto));
        }

        public async Task<ApiResponse<IEnumerable<HadithChapterDto>>> GetChaptersByBookAsync(int bookId)
        {
           var chapters=  await _unitOfWork.Hadiths.GetChaptersByBookAsync(bookId);
            if(chapters==null)
                return ApiResponse<IEnumerable<HadithChapterDto>>.Fail("No chapters found for the specified book.");

            return ApiResponse<IEnumerable<HadithChapterDto>>.Ok(chapters.Select(MapHadithChapterToDto));

        }

        public async Task<ApiResponse<PaginatedResult<HadithDto>>> GetByBookPagedAsync(int bookId, PaginationParams pagination)
        {
            var (data, total) = await _unitOfWork.Hadiths.GetByBookPagedAsync(
             bookId, pagination.Page, pagination.PageSize);

            var result = PaginatedResult<HadithDto>.Create(
                data.Select(MapHadithToDto), pagination.Page, pagination.PageSize, total);

            return ApiResponse<PaginatedResult<HadithDto>>.Ok(result);
        }

        public async Task<ApiResponse<PaginatedResult<HadithDto>>> SearchPagedAsync(string keyword, PaginationParams pagination)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return ApiResponse<PaginatedResult<HadithDto>>.Fail("Keyword is required");

            var (data, total) = await _unitOfWork.Hadiths.SearchPagedAsync(
                keyword, pagination.Page, pagination.PageSize);

            var result = PaginatedResult<HadithDto>.Create(
                data.Select(MapHadithToDto), pagination.Page, pagination.PageSize, total);

            return ApiResponse<PaginatedResult<HadithDto>>.Ok(result);
        }
        //==Mapping
        private static HadithDto MapHadithToDto(Hadith h) => new()
        {
            Id = h.Id,
            Number = h.Number,
            TextArabic = h.TextArabic,
            TextEnglish = h.TextEnglish,
            Narrator = h.Narrator,
            Grade = h.Grade,
            ChapterId = h.ChapterId,
            ChapterName = h.Chapter?.NameArabic??string.Empty,
            BookName = h.Chapter?.Book.NameArabic??string.Empty
        };
        private static HadithChapterDto MapHadithChapterToDto(HadithChapter h) => new()
        {
            Id = h.Id,
            NameArabic = h.NameArabic,
            NameEnglish = h.NameEnglish,
            BookId = h.BookId,
            BookName = h.Book?.NameArabic ?? string.Empty

        };
        private static HadithBookDto MapHadithBookToDto(HadithBook h) => new()
        {
            Id = h.Id,
            NameEnglish = h.NameEnglish,
            NameArabic = h.NameArabic,
            Author = h.Author

        };

    }
}
