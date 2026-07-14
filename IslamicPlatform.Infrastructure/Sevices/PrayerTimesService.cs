using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.PrayerTimesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public class PrayerTimesService : IPrayerTimesService
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheService _cache;

        private readonly Dictionary<string, string> _prayerNamesArabic = new()
        {
            {"Fajr","الفجر" },
            {"Sunrise","الشروق" },
            {"Dhuhr","الظهر" },
            {"Asr","العصر" },
            {"Maghrib","المغرب" },
            {"Isha","العشاء" }

        };
        public PrayerTimesService(HttpClient httpClient,ICacheService cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }
        
        public async Task<ApiResponse<PrayerTimesDTO>> GetPrayerTimesAsync(double latitude, double longitude)
        {
            var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var cacheKey = $"Prayer:{latitude:F2} : {longitude:F2} : {today}";

            var cached = await _cache.GetAsync<PrayerTimesDTO>(cacheKey);
            if (cached is not null)
                return ApiResponse<PrayerTimesDTO>.Ok(cached);

            var Url = $"https://api.aladhan.com/v1/timings?latitude={latitude}&longitude={longitude}&method=4";
            var response = await _httpClient.GetAsync(Url);

            if (!response.IsSuccessStatusCode)
                return ApiResponse<PrayerTimesDTO>.Fail("Failed to get prayer times");

            var content = await response.Content.ReadAsStringAsync();//Api=>Json
            var json = JsonSerializer.Deserialize<AladhanResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true//NO Case sensitive
            });

            if (json?.Data?.Timings == null)
                return ApiResponse<PrayerTimesDTO>.Fail("Invalid response from prayer times API");

            var dto = new PrayerTimesDTO
            {
                Fajr=json.Data.Timings.Fajr,
                Sunrise=json.Data.Timings.Sunrise,
                Dhuhr= json.Data.Timings.Dhuhr,
                Asr= json.Data.Timings.Asr,
                Maghrib= json.Data.Timings.Maghrib,
                Isha= json.Data.Timings.Isha,
                Date= json.Data.Date?.Readable??today,
                City=json.Data.Meta?.Timezone??"",
                Country=""

            };

            var midnight = DateTime.UtcNow.Date.AddDays(1) - DateTime.UtcNow;
            await _cache.SetAsync(cacheKey, dto, midnight);

            return ApiResponse<PrayerTimesDTO>.Ok(dto);
        }
        public async Task<ApiResponse<NextPrayerDTO>> GetNextPrayerAsync(double latitude, double longitude)
        {
            var timeResult = await GetPrayerTimesAsync(latitude, longitude);
            if (!timeResult.Success)
                return ApiResponse<NextPrayerDTO>.Fail(timeResult.Message);

            var times = timeResult.Data!;
            var now = DateTime.Now.TimeOfDay;

            var prayers = new Dictionary<string, string>
            {
                {"Fajr",times.Fajr },
              //  {"Sunrise",times.Sunrise },
                {"Dhuhr",times.Dhuhr },
                {"Asr",times.Asr },
                {"Maghrib",times.Maghrib },
                {"Isha",times.Isha },
            };

            foreach(var prayer in prayers)
            {
                if(TimeSpan.TryParse(prayer.Value,out var prayerTime)&& prayerTime > now)
                {
                    var remaining = prayerTime - now;
                    return ApiResponse<NextPrayerDTO>.Ok(new NextPrayerDTO
                    {
                        PrayerName = prayer.Key,
                        PrayerNameArabic = _prayerNamesArabic[prayer.Key],
                        Time = prayer.Value,
                        RemainingTime = $"{(int)remaining.TotalHours}h {remaining.Minutes}m"

                    });
                }
            }

            return ApiResponse<NextPrayerDTO>.Ok(new NextPrayerDTO
            {
                PrayerName = "Fajr",
                PrayerNameArabic = "الفجر",
                Time = times.Fajr,
                RemainingTime = "Tomorrow"
            });
        }

        
        //Models For=> aladhan.com Response 
        public class AladhanResponse
        {
            public AladhanData? Data {  get; set; }
        }
        public class AladhanData
        {
            public AladhanTimings? Timings { get; set; }
            public AladhanDate? Date { get; set; }
            public AladhanMeta? Meta { get; set; }
        }
        public class AladhanTimings
        {
            public string Fajr { get; set; }
            public string Sunrise { get; set; }
            public string Dhuhr { get; set; }
            public string Asr { get; set; }
            public string Maghrib { get; set; }
            public string Isha { get; set; }
            
        }
        public class AladhanDate
        {
            public string Readable { get; set; }//=>(13 June 2026)
            public AladhanHijri? Hijri { get; set; }
        }
        public class AladhanHijri
        {
            public string Date {  get; set; }
            public AladhanHijriMonth? Month { get; set; }
            public string Year { get; set; }
        }
        public class AladhanHijriMonth
        {
            public string En { get; set; }
            public string Ar { get; set; }
        }
        public class AladhanMeta
        {
            public string Timezone {  get; set; }
        }
        
    }
}
