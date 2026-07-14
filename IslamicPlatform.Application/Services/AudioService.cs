
using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AudioDTOs;
using IslamicPlatform.Application.DTOs.Quran;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.Sheikh;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Services
{
    
    public class AudioService : IAudioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;

        public AudioService(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ApiResponse<IEnumerable<SheikhDto>>> GetAllSheikhsAsync()
        {
            var sheikhs = await _unitOfWork.Sheikhs.GetAllAsync();
            return ApiResponse<IEnumerable<SheikhDto>>.Ok(sheikhs.Select(MapSheikhToDto));
        }

        public async Task<ApiResponse<IEnumerable<RecitationDto>>> GetSheikhRecitationsAsync(int sheikhId)
        {
            var sheikh = await _unitOfWork.Sheikhs.GetWithRecitationsAsync(sheikhId);
            if (sheikh == null)
                return ApiResponse<IEnumerable<RecitationDto>>.Fail("Sheikh not found");

            return ApiResponse<IEnumerable<RecitationDto>>.Ok(
                sheikh.Recitations.Select(MapRecitationToDto));
        }
        public async Task<ApiResponse<RecitationDto>> GetRecitationAsync(int sheikhId, int surahId)
        {
            var recitation = await _unitOfWork.Sheikhs.GetRecitationAsync(sheikhId, surahId);
            if (recitation == null)
                return ApiResponse<RecitationDto>.Fail("Recitation not found");

            return ApiResponse<RecitationDto>.Ok(MapRecitationToDto(recitation));
        }


        public async Task<ApiResponse<SheikhDto>> UploadSheikhImageAsync(int sheikhId, IFormFile file)
        {
            var sheikh = await _unitOfWork.Sheikhs.GetByIdAsync(sheikhId);
            if (sheikh == null)
                return ApiResponse<SheikhDto>.Fail("Sheikh not found");

            // لو عنده صورة قديمة امسحها الأول
            if (!string.IsNullOrEmpty(sheikh.ImagePublicId))
                await _cloudinaryService.DeleteImageAsync(sheikh.ImagePublicId);

            // ارفع الصورة الجديدة
            var result = await _cloudinaryService.UploadImageAsync(file, "sheikhs");

            // حدث الـ Sheikh في الـ DB
            sheikh.ImageUrl = result.Url;
            sheikh.ImagePublicId = result.PublicId;

            await _unitOfWork.Sheikhs.UpdateAsync(sheikh);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<SheikhDto>.Ok(MapSheikhToDto(sheikh), "Image uploaded successfully");
        }

        // ===== Private Mapping Methods =====

        private static SheikhDto MapSheikhToDto(Sheikh s) => new()
        {
            Id = s.Id,
            NameArabic = s.NameArabic,
            NameEnglish = s.NameEnglish,
            Country = s.Country,
            Bio = s.Bio,
            ImageUrl = s.ImageUrl,
            MoshafType = s.MoshafType
        };

        private static RecitationDto MapRecitationToDto(Recitation r) => new()
        {
            Id = r.Id,
            AudioUrl = r.AudioUrl,
            Quality = r.Quality,
            DurationSeconds = r.DurationSeconds,
            Sheikh = r.Sheikh == null ? null! : MapSheikhToDto(r.Sheikh),
            Surah = r.Surah == null ? null! : new SurahDto
            {
                Id = r.Surah.Id,
                Number = r.Surah.Number,
                NameArabic = r.Surah.NameArabic,
                NameEnglish = r.Surah.NameEnglish,
                NameTransliteration = r.Surah.NameTransliteration,
                AyahCount = r.Surah.AyahCount,
                RevelationType = r.Surah.RevelationType.ToString(),
                JuzNumber = r.Surah.JuzNumber
            }
        };
    }

}

