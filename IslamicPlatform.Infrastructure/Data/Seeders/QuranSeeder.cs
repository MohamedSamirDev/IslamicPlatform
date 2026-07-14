using IslamicPlatform.Application.Helpers;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Data.Seed
{
    // IslamicPlatform.Infrastructure/Data/Seeders/QuranSeeder.cs
    public class QuranSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        // alquran.cloud API — مجاني ومش محتاج API Key
        private const string BaseUrl = "https://api.alquran.cloud/v1";

        public QuranSeeder(ApplicationDbContext context, HttpClient httpClient, ILogger logger)
        {
            _context = context;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            // لو القرآن موجود في الـ DB خلاص مش هنعمل Seed تاني
            if (await _context.Surahs.AnyAsync())
            {
                _logger.LogInformation("القرآن موجود في الـ Database — تم تخطي الـ Seed");
                return;
            }

            _logger.LogInformation("بدء Seed القرآن الكريم...");

            try
            {
                // جيب القرآن كامل بالعربي
                var arabicQuran = await FetchQuranAsync("quran-uthmani");

                // جيب الترجمة الإنجليزية
                var englishQuran = await FetchQuranAsync("en.asad");

                // جيب تفسير السعدي
                var tafseerQuran = await FetchQuranAsync("ar.muyassar");

                if (arabicQuran == null || englishQuran == null)
                {
                    _logger.LogError("فشل في جلب القرآن من الـ API");
                    return;
                }

                var surahs = new List<Surah>();

                foreach (var surahData in arabicQuran.Surahs)
                {
                    var surah = new Surah
                    {
                        Number = surahData.Number,
                        NameArabic = surahData.Name,
                        NameEnglish = surahData.EnglishName,
                        NameTransliteration = surahData.EnglishNameTranslation,
                        NameArabicNormalized = ArabicSearchHelper.NormalizeArabic(surahData.Name),
                        AyahCount = surahData.Ayahs.Count,
                        RevelationType = surahData.RevelationType == "Meccan"
                            ? RevelationType.Meccan
                            : RevelationType.Medinan,
                        JuzNumber = surahData.Ayahs.First().Juz,
                        Ayahs = new List<Ayah>()
                    };

                    var englishSurah = englishQuran.Surahs.FirstOrDefault(s => s.Number == surahData.Number);
                    var tafseerSurah = tafseerQuran?.Surahs.FirstOrDefault(s => s.Number == surahData.Number);

                    for (int i = 0; i < surahData.Ayahs.Count; i++)
                    {
                        var ayahData = surahData.Ayahs[i];

                        var ayah = new Ayah
                        {
                            Number = ayahData.Number,
                            NumberInSurah = ayahData.NumberInSurah,
                            TextArabic = ayahData.Text,
                            TextArabicNormalized = ArabicSearchHelper.NormalizeArabic(ayahData.Text), // ← ضيف دي
                            JuzNumber = ayahData.Juz,
                            HizbNumber = (int)Math.Ceiling(ayahData.Hizbquarter / 4.0),
                            RubNumber = ayahData.Hizbquarter,
                            Translations = new List<AyahTranslation>(),
                            Tafseers = new List<Tafseer>()
                        };

                        // ضيف الترجمة الإنجليزية
                        if (englishSurah != null && i < englishSurah.Ayahs.Count)
                        {
                            ayah.Translations.Add(new AyahTranslation
                            {
                                Text = englishSurah.Ayahs[i].Text,
                                Language = "en",
                                TranslatorName = "Muhammad Asad"
                            });
                        }

                        // ضيف التفسير
                        if (tafseerSurah != null && i < tafseerSurah.Ayahs.Count)
                        {
                            ayah.Tafseers.Add(new Tafseer
                            {
                                Text = tafseerSurah.Ayahs[i].Text,
                                ScholarName = "تفسير الميسر"
                            });
                        }

                        surah.Ayahs.Add(ayah);
                    }

                    surahs.Add(surah);
                    _logger.LogInformation($"تم تجهيز سورة {surah.NameArabic}");
                }

                // Save كل حاجة في الـ DB دفعة واحدة
                await _context.Surahs.AddRangeAsync(surahs);
                await _context.SaveChangesAsync();

                _logger.LogInformation("✅ تم Seed القرآن الكريم بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حصل خطأ أثناء Seed القرآن");
                throw;
            }
        }

        private async Task<AlQuranResponse?> FetchQuranAsync(string edition)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/quran/{edition}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AlQuranApiResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Data;
        }
    }

    // Models للـ alquran.cloud API Response
    public class AlQuranApiResponse
    {
        public AlQuranResponse? Data { get; set; }
    }

    public class AlQuranResponse
    {
        public List<AlQuranSurah> Surahs { get; set; } = new();
    }

    public class AlQuranSurah
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string EnglishNameTranslation { get; set; }
        public string RevelationType { get; set; }
        public List<AlQuranAyah> Ayahs { get; set; } = new();
    }

    public class AlQuranAyah
    {
        public int Number { get; set; }
        public int NumberInSurah { get; set; }
        public string Text { get; set; }
        public int Juz { get; set; }
        public int Hizbquarter { get; set; }
        public int Page { get; set; }
    }
}
