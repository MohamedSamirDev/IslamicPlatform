using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.Quran;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Services
{
    public class QuranService:IQuranService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cache;
        private const string AllSurahsCacheKey = "quran:surahs:all";
        private const string SurahCacheKey = "quran:surahs:{0}";      // {0} = number
        private const string AyahsCacheKey = "quran:ayahs:surah:{0}";// {0} = surahId
        public QuranService(IUnitOfWork unitOfWork, ICacheService cache )
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }
        public async Task<ApiResponse<IEnumerable<SurahDto>>> GetAllSurahsAsync()
        {
          var cache= await _cache.GetAsync<IEnumerable<SurahDto>>(AllSurahsCacheKey);
            if (cache is not null)
                return ApiResponse<IEnumerable<SurahDto>>.Ok(cache);

            var surahs = await _unitOfWork.Surahs.GetAllAsync();
            var dto = surahs.Select(MapSurahToDto).ToList();

            await _cache.SetAsync(AllSurahsCacheKey, dto, TimeSpan.FromDays(7));
            return ApiResponse<IEnumerable<SurahDto>>.Ok(dto);

        }

        public async Task<ApiResponse<IEnumerable<AyahDto>>> GetAyahsBySurahAsync(int surahId)
        {
           var cacheKey = string.Format(AyahsCacheKey, surahId);
            var cached = await _cache.GetAsync<IEnumerable<AyahDto>>(cacheKey);
            if (cached is not null)
                return ApiResponse<IEnumerable<AyahDto>>.Ok(cached);

            var ayahs = await _unitOfWork.Ayahs.GetBySurahAsync(surahId);
            var dto = ayahs.Select(MapAyahToDto).ToList();

            await _cache.SetAsync(cacheKey, dto, TimeSpan.FromDays(7));
            return ApiResponse<IEnumerable<AyahDto>>.Ok(dto);

        }

        public async Task<ApiResponse<AyahDto>> GetAyahWithTafseerAsync(int ayahId)
        {
          var ayahs=  await _unitOfWork.Ayahs.GetWithTafseerAsync(ayahId);
            if (ayahs == null)
                return ApiResponse<AyahDto>.Fail("Ayah not found");

            return ApiResponse<AyahDto>.Ok(MapAyahToDto(ayahs));
        }

        public async Task<ApiResponse<IEnumerable<AyahDto>>> GetByJuzAsync(int juzNumber)
        {
            var juz = await _unitOfWork.Ayahs.GetByJuzAsync(juzNumber);
            if (juz is null)
                return ApiResponse<IEnumerable<AyahDto>>.Fail("Juz not found");

            return ApiResponse<IEnumerable<AyahDto>>.Ok(juz.Select(MapAyahToDto).ToList());
        }

        public async Task<ApiResponse<SurahDto>> GetSurahByNumberAsync(int number)
        {
            var surah = await _unitOfWork.Surahs.GetByNumberAsync(number);
            if (surah == null)
                return ApiResponse<SurahDto>.Fail("Surah not found");

            return ApiResponse<SurahDto>.Ok(MapSurahToDto(surah));
        }

        public async Task<ApiResponse<IEnumerable<AyahDto>>> SearchAyahsAsync(string keyword)
        {
            var ayahs = await _unitOfWork.Ayahs.SearchAsync(keyword);
            return ApiResponse<IEnumerable<AyahDto>>.Ok(ayahs.Select(MapAyahToDto));
        }

        public async Task<ApiResponse<IEnumerable<SurahDto>>> SearchSurahsAsync(string keyword)
        {
            var surahs = await _unitOfWork.Surahs.SearchAsync(keyword);
            return ApiResponse<IEnumerable<SurahDto>>.Ok(surahs.Select(MapSurahToDto));
        }


        // ===== Private Mapping Methods =====
        private static AyahDto MapAyahToDto(Ayah a) => new()
        {
            Id= a.Id,
            Number= a.Number,
            NumberInSurah= a.NumberInSurah,
            TextArabic= a.TextArabic,
            JuzNumber= a.JuzNumber,
            HizbNumber= a.HizbNumber,
            RubNumber= a.RubNumber,
            Translations=a.Translations?.Select(t=>new AyahTranslationDto
            {
                Text= t.Text,
                TranslatorName= t.TranslatorName,
                Language=t.Language
            }).ToList()??new(), //or []

            Tafseers=a.Tafseers?.Select(t=>new TafseerDto
            {
                Text=t.Text,
                ScholarName=t.ScholarName
            }).ToList()??new()
           

        };

        private static SurahDto MapSurahToDto(Surah s) => new()
        {
            Id=s.Id,
            Number=s.Number,
            NameArabic=s.NameArabic,
            NameEnglish=s.NameEnglish,
            NameTransliteration =s.NameTransliteration,
            AyahCount=s.AyahCount,
            RevelationType=s.RevelationType.ToString(),
            JuzNumber=s.JuzNumber

        };

    }
}
