using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AudioDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public interface IAudioService
    {
        Task<ApiResponse<IEnumerable<SheikhDto>>> GetAllSheikhsAsync();
        Task<ApiResponse<RecitationDto>> GetRecitationAsync(int sheikhId, int surahId);
        Task<ApiResponse<IEnumerable<RecitationDto>>> GetSheikhRecitationsAsync(int sheikhId);
        Task<ApiResponse<SheikhDto>> UploadSheikhImageAsync(int sheikhId, IFormFile file);
    }
}

