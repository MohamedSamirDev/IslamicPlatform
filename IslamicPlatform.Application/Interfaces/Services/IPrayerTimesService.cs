using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.PrayerTimesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public interface IPrayerTimesService
    {
        Task <ApiResponse<PrayerTimesDTO>> GetPrayerTimesAsync(double latitude, double longitude);
        Task <ApiResponse<NextPrayerDTO>> GetNextPrayerAsync(double latitude, double longitude);
    }
}
