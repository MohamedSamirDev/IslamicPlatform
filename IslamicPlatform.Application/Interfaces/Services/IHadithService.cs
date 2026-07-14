using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.Common;
using IslamicPlatform.Application.DTOs.HadithDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public interface IHadithService
    {
        Task<ApiResponse<IEnumerable<HadithDto>>> SearchAsync(string keyword);
        Task<ApiResponse<IEnumerable<HadithDto>>> GetByBookAsync(int bookId);
        Task<ApiResponse<IEnumerable<HadithBookDto>>> GetAllBooksAsync();
        Task<ApiResponse<IEnumerable<HadithDto>>> GetByChapterAsync(int chapterId);
        Task<ApiResponse<IEnumerable<HadithChapterDto>>> GetChaptersByBookAsync(int bookId);
        //PaginatedResult
        Task<ApiResponse<PaginatedResult<HadithDto>>> GetByBookPagedAsync(int bookId, PaginationParams pagination);
        Task<ApiResponse<PaginatedResult<HadithDto>>> SearchPagedAsync(string keyword, PaginationParams pagination);


    }
}
