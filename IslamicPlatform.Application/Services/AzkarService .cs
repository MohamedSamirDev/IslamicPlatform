using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AzkarDTOs;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.Azkar;
using IslamicPlatform.Domain.Enums;
using IslamicPlatform.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Services
{
    public class AzkarService : IAzkarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cache;
        private const string AzkarCacheKey = "azkar:{0}"; // {0} = category
        public AzkarService(IUnitOfWork unitOfWork, ICacheService cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<ApiResponse<IEnumerable<ZikrDto>>> GetByCategoryAsync(ZikrCategory category)
        {
            var cacheKey = string.Format(AzkarCacheKey, category);
            var cached = await _cache.GetAsync<IEnumerable<ZikrDto>>(cacheKey);

          
            if (cached is not null && cached.Any())
                return ApiResponse<IEnumerable<ZikrDto>>.Ok(cached);

            var azkar = await _unitOfWork.Azkar.GetByCategoryAsync(category);
            var dto = azkar.Select(z => new ZikrDto
            {
                Id = z.Id,
                TextArabic = z.TextArabic,
                TextTransliteration = z.TextTransliteration,
                Meaning = z.Meaning,
                Source = z.Source,
                RepeatCount = z.RepeatCount,
                Category = z.Category.ToString()
            }).ToList();

            await _cache.SetAsync(cacheKey, dto, TimeSpan.FromDays(1));
            return ApiResponse<IEnumerable<ZikrDto>>.Ok(dto);
        }

        public async Task<ApiResponse<IEnumerable<ZikrProgressDto>>> GetUserProgressAsync(string userId, ZikrCategory category)
        {
            var progresses = await _unitOfWork.Azkar.GetAllUserProgressAsync(userId, category);
            var allAzkar = await _unitOfWork.Azkar.GetByCategoryAsync(category);

            var dto = allAzkar.Select(z =>
            {
                var progress = progresses.FirstOrDefault(p => p.ZikrId == z.Id);
                return new ZikrProgressDto
                {
                    ZikrId = z.Id,
                    TextArabic = z.TextArabic,
                    RepeatCount = z.RepeatCount,
                    CurrentCount = progress?.CurrentCount ?? 0,
                    IsCompleted = progress?.IsCompleted ?? false
                };
            });
            return ApiResponse<IEnumerable<ZikrProgressDto>>.Ok(dto);
        }

        public async Task<ApiResponse<ZikrProgressDto>> IncrementCountAsync(string userId, int zikrId)
        {
            var zikr = await _unitOfWork.Azkar.GetByIdAsync(zikrId);
            if (zikr is null)
               
                return ApiResponse<ZikrProgressDto>.Fail("Zikr not found");

            var progress = await _unitOfWork.Azkar.GetUserProgressAsync(userId, zikrId);

            if (progress is not null && progress.IsCompleted)
            {
                return ApiResponse<ZikrProgressDto>.Ok(new ZikrProgressDto
                {
                    ZikrId = zikrId,
                    TextArabic = zikr.TextArabic,
                    RepeatCount = zikr.RepeatCount,
                    CurrentCount = progress.CurrentCount,
                    IsCompleted = true
                });
            }
            if (progress == null)
            {
                progress = new UserZikrProgress
                {
                    UserId = userId,
                    ZikrId = zikrId,
                    CurrentCount = 1,
                    IsCompleted = zikr.RepeatCount == 1,
                    LastResetDate = DateTime.UtcNow
                };
                await _unitOfWork.Azkar.AddProgressAsync(progress);
            }
            else
            {
                progress.CurrentCount++;
                progress.IsCompleted = progress.CurrentCount >= zikr.RepeatCount;
            }

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<ZikrProgressDto>.Ok(new ZikrProgressDto
            {
                ZikrId = zikrId,
                TextArabic = zikr.TextArabic,
                RepeatCount = zikr.RepeatCount,
                CurrentCount = progress.CurrentCount,
                IsCompleted = progress.IsCompleted
            });
        }

        public async Task<ApiResponse<bool>> ResetDailyProgressAsync(string userId)
        {
            await _unitOfWork.Azkar.ResetDailyProgressAsync(userId);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.Ok(true, "Daily progress reset successfully");
        }

        private static ZikrDto MapZikerToDto(Zikr z) => new()
        {
            Id = z.Id,
            TextArabic = z.TextArabic,
            TextTransliteration = z.TextTransliteration,
            Meaning = z.Meaning,
            Source = z.Source,
            RepeatCount = z.RepeatCount,
            Category = z.Category.ToString()
        };
    }
}
