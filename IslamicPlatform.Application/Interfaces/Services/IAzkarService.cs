using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AzkarDTOs;
using IslamicPlatform.Domain.Entites.Azkar;
using IslamicPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public interface IAzkarService
    {
        Task<ApiResponse<IEnumerable<ZikrDto>>> GetByCategoryAsync(ZikrCategory category);
        Task<ApiResponse<IEnumerable<ZikrProgressDto>>> GetUserProgressAsync(string userId, ZikrCategory category);
        Task<ApiResponse<ZikrProgressDto>> IncrementCountAsync(string userId, int zikrId);
        Task<ApiResponse<bool>> ResetDailyProgressAsync(string userId);
    }
}
