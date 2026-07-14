using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.IdentityDTOs;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Services
{
    public class ReadingProgressService : IReadingProgressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReadingProgressService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<ApiResponse<ReadingProgressDto>> GetProgressAsync(string userId)
        {
            var progress = await _unitOfWork.ReadingProgresses.GetByUserIdAsync(userId);
            if (progress == null)
                return ApiResponse<ReadingProgressDto>.Fail("No reading progress found");

            var surah = await _unitOfWork.Surahs.GetByIdAsync(progress.LastSurahId);
            var ayah = await _unitOfWork.Ayahs.GetByIdAsync(progress.LastAyahId);

            return ApiResponse<ReadingProgressDto>.Ok(new ReadingProgressDto
            {
                LastSurahId = progress.LastSurahId,
                LastSurahName = surah?.NameArabic ?? string.Empty,
                LastAyahId = progress.LastAyahId,
                LastAyahNumber = ayah?.NumberInSurah ?? 0,
                UpdatedAt = progress.UpdatedAt
            });
        }

        public async Task<ApiResponse<ReadingProgressDto>> UpdateProgressAsync(string userId, int surahId, int ayahId)
        {
            var surah = await _unitOfWork.Surahs.GetByIdAsync(surahId);
            if (surah == null)
                return ApiResponse<ReadingProgressDto>.Fail("Surah not found");

            var ayah = await _unitOfWork.Ayahs.GetByIdAsync(ayahId);
            if (ayah == null)
                return ApiResponse<ReadingProgressDto>.Fail("Ayah not found");

            var progress = await _unitOfWork.ReadingProgresses.GetByUserIdAsync(userId);

            if (progress == null)
            {
                progress = new ReadingProgress
                {
                    UserId = userId,
                    LastSurahId = surahId,
                    LastAyahId = ayahId,
                    UpdatedAt = DateTime.UtcNow
                };
                await _unitOfWork.ReadingProgresses.AddAsync(progress);
            }
            else
            {
                progress.LastSurahId = surahId;
                progress.LastAyahId = ayahId;
                progress.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.ReadingProgresses.UpdateAsync(progress);
            }

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<ReadingProgressDto>.Ok(new ReadingProgressDto
            {
                LastSurahId = surahId,
                LastSurahName = surah.NameArabic,
                LastAyahId = ayahId,
                LastAyahNumber = ayah.NumberInSurah,
                UpdatedAt = progress.UpdatedAt
            }, "Progress updated");
        }
    }
}
