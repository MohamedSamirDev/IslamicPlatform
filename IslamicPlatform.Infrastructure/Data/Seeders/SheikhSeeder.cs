using IslamicPlatform.Domain.Entites.Sheikh;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace IslamicPlatform.Infrastructure.Data.Seeders
{
    public class SheikhSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        private static readonly HashSet<string> FamousSheikhNames = new()
        {
            "محمد صديق المنشاوي",
            "عبد الباسط عبد الصمد",
            "محمود خليل الحصري",
            "مصطفى إسماعيل",
            "محمد رفعت",
            "أبو العينين شعيشع",
            "محمد محمود الطبلاوي",
            "عبد الرحمن السديس",
            "سعود الشريم",
            "ماهر المعيقلي",
            "ياسر الدوسري",
            "عبد الله الجهني",
            "مشاري راشد العفاسي",
            "ناصر القطامي",
            "عادل الكلباني",
            "عبد المحسن القاسم",
            "حمدي الزامل",
            "أحمد خليل شاهين",
            "خالد القحطاني",
            "إدريس أبكر",
            "عبد الولي الأركاني",
            "صلاح البدير",
            "عمر القزابري",
            "أحمد بن علي العجمي",
            "أبو بكر الشاطري",
            "محمد أيوب",
            "صلاح بوخاطر",
            "أحمد نعينع",
            "سعد الغامدي",
            "هاني الرفاعي",
            "توفيق الصايغ",
            "فارس عباد",
            "علي جابر",
            "بندر بليلة",
            "خالد الجليل",
            "إسلام صبحي",
            "ياسر الفيومي",
            "عبد الله بصفر",
            "وليد الشامي",
            "محمد المحيسني",
            "محمد جبريل",
            "عبد الباري الثبيتي",
            "علي الحذيفي",
            "يوسف الزهراني",
            "خالد عبد الكافي",
            "عبد العزيز الزهراني",
            "عبد الله المطرود",
            "أحمد طالب بن حميد",
            "نورين محمد صديق",
            "مفتاح السلطاني",
            "عبد الرحمن السويد",
            "محمد عبد الكريم",
            "محمد سعيد",
            "مصطفى اللاهوني",
            "محمد الرفاعي",
            "محمود علي البنا",
        };

        public SheikhSeeder(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        private static string Normalize(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;

            return name
                .Replace(" ", "")
                .Replace("أ", "ا")
                .Replace("إ", "ا")
                .Replace("آ", "ا")
                .Replace("ٱ", "ا")
                .Replace("ة", "ه")
                .Replace("ى", "ي")
                .Replace("ـ", "")
                .Trim();
        }

        public async Task SeedAsync()
        {
            if (await _context.Sheikhs.AnyAsync())
            {
                _logger.LogInformation("الشيوخ موجودين — تم تخطي الـ Seed");
                return;
            }

            _logger.LogInformation("بدء Seed الشيوخ...");

            using var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(10)
            };

            var url = "https://www.mp3quran.net/api/v3/reciters?language=ar";
            var response = await httpClient.GetStringAsync(url);
            var json = JsonSerializer.Deserialize<Mp3QuranResponse>(response);

            if (json?.reciters == null)
            {
                _logger.LogError("فشل جلب القراء من الـ API");
                return;
            }

            _logger.LogInformation($"تم جلب {json.reciters.Count} قارئ من الـ API");

            var famousMap = FamousSheikhNames
                .GroupBy(n => Normalize(n))
                .ToDictionary(g => g.Key, g => g.First());

            var allSheikhs = new List<Sheikh>();
            var matchedOriginalNames = new HashSet<string>();

            foreach (var reciter in json.reciters)
            {
                var normalizedApiName = Normalize(reciter.name);

                if (!famousMap.ContainsKey(normalizedApiName))
                    continue;

                if (reciter.moshaf == null || !reciter.moshaf.Any())
                {
                    _logger.LogWarning($"⚠️ {reciter.name} ملوش أي Moshaf");
                    continue;
                }

             
                var bestMoshaf = reciter.moshaf
                    .OrderByDescending(m => m.surah_total)
                    .ThenByDescending(m => m.name.Contains("مرتل"))
                    .FirstOrDefault();

                if (bestMoshaf == null)
                {
                    _logger.LogWarning($"⚠️ {reciter.name} ملوش أي موشف صالح");
                    continue;
                }

                try
                {
                    var recitations = BuildRecitations(bestMoshaf);

                    if (!recitations.Any())
                    {
                        _logger.LogWarning($"⚠️ {reciter.name}: مفيش سور اتبنت — اتشال");
                        continue;
                    }

                    allSheikhs.Add(new Sheikh
                    {
                        NameArabic = reciter.name,
                        NameEnglish = reciter.name,
                        Country = "غير محدد",
                        Bio = bestMoshaf.name,
                        MoshafType = bestMoshaf.name,
                        Recitations = recitations
                    });

                    matchedOriginalNames.Add(famousMap[normalizedApiName]);

                    if (recitations.Count < 114)
                    {
                        _logger.LogWarning($"ℹ️ {reciter.name}: تلاوة غير مكتملة ({recitations.Count} من 114 سورة)");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"خطأ في {reciter.name}: {ex.Message}");
                }
            }

            var missing = FamousSheikhNames.Except(matchedOriginalNames).ToList();
            if (missing.Any())
            {
                _logger.LogWarning($"⚠️ الأسامي الناقصة ({missing.Count} من {FamousSheikhNames.Count}): {string.Join(" | ", missing)}");
            }

            await _context.Sheikhs.AddRangeAsync(allSheikhs);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"✅ تم حفظ {allSheikhs.Count} شيخ بنجاح (من أصل {FamousSheikhNames.Count} في الليستة)");
        }

        private List<Recitation> BuildRecitations(Moshaf moshaf)
        {
            var recitations = new List<Recitation>();

            var surahNumbers = moshaf.surah_list?
                .Split(',')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => int.TryParse(s.Trim(), out var n) ? n : 0)
                .Where(n => n > 0)
                .ToList() ?? new List<int>();

            foreach (var surahNum in surahNumbers)
            {
                recitations.Add(new Recitation
                {
                    SurahId = surahNum,
                    AudioUrl = $"{moshaf.server}{surahNum:D3}.mp3",
                    Quality = "128kbps",
                    DurationSeconds = 0
                });
            }

            return recitations;
        }
    }

    internal class Mp3QuranResponse
    {
        public List<Reciter> reciters { get; set; }
    }

    internal class Reciter
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Moshaf> moshaf { get; set; }
    }

    internal class Moshaf
    {
        public string name { get; set; }
        public string server { get; set; }
        public int surah_total { get; set; }
        public string surah_list { get; set; }
    }
}