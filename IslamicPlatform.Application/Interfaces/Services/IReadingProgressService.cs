using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public interface IReadingProgressService
    {
        Task<ApiResponse<ReadingProgressDto>> GetProgressAsync(string userId);
        Task<ApiResponse<ReadingProgressDto>> UpdateProgressAsync(string userId, int surahId, int ayahId);


    }
}
