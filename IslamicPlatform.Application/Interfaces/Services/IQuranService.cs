using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.Quran;
using IslamicPlatform.Domain.Entites.Quran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public interface IQuranService
    {
        Task<ApiResponse<IEnumerable<SurahDto>>> GetAllSurahsAsync();
        Task<ApiResponse<SurahDto>> GetSurahByNumberAsync(int number);
        Task<ApiResponse<IEnumerable<AyahDto>>> GetAyahsBySurahAsync(int surahId);
        Task<ApiResponse<AyahDto>> GetAyahWithTafseerAsync(int ayahId);
        Task<ApiResponse<IEnumerable<AyahDto>>> GetByJuzAsync(int juzNumber);
        Task<ApiResponse<IEnumerable<SurahDto>>> SearchSurahsAsync(string keyword);
        Task<ApiResponse<IEnumerable<AyahDto>>> SearchAyahsAsync(string keyword);

    }
}
