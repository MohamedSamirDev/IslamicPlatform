using IslamicPlatform.Application.Helpers;
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public class HadithSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;

    private const string BaseUrl = "https://raw.githubusercontent.com/AhmedBaset/hadith-json/v1.2.0/db/by_book/the_9_books";

    // قائمة الكتب التسعة كاملة
    private readonly List<(string File, string NameAr, string NameEn, string Author)> _collections = new()
    {
        ("bukhari.json",  "صحيح البخاري",  "Sahih Al-Bukhari",  "محمد بن إسماعيل البخاري"),
        ("muslim.json",   "صحيح مسلم",     "Sahih Muslim",      "مسلم بن الحجاج"),
        ("abudawud.json", "سنن أبي داود",  "Sunan Abi Dawud",   "أبو داود السجستاني"),
        ("tirmidhi.json", "جامع الترمذي",  "Jami` at-Tirmidhi", "محمد بن عيسى الترمذي"),
        ("nasai.json",    "سنن النسائي",   "Sunan an-Nasa'i",   "أحمد بن شعيب النسائي"),
        ("ibnmajah.json", "سنن ابن ماجه", "Sunan Ibn Majah",   "محمد بن يزيد ابن ماجه"),
        ("ahmed.json",    "مسند أحمد",     "Musnad Ahmad",      "أحمد بن حنبل"),
        ("malik.json",    "موطأ مالك",     "Muwatta Malik",     "مالك بن أنس"),
        ("darimi.json",   "سنن الدارمي",   "Sunan al-Darimi",   "عبد الله بن عبد الرحمن الدارمي"),
    };

    public HadithSeeder(ApplicationDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromMinutes(30); // زيادة الوقت لأن مسند أحمد ضخم جداً

        foreach (var collection in _collections)
        {
            try
            {
                // التأكد إذا كان الكتاب موجود مسبقاً لتجنب التكرار
                if (await _context.HadithBooks.AnyAsync(b => b.NameArabic == collection.NameAr))
                {
                    Console.WriteLine($"[INFO] {collection.NameAr} موجود بالفعل. تم التخطي.");
                    continue;
                }

                Console.WriteLine($"[START] جاري تحميل ومعالجة: {collection.NameAr}...");
                await SeedCollectionAsync(httpClient, collection);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CRITICAL ERROR] {collection.NameAr}: {ex.Message}");
            }
        }

        Console.WriteLine("✅ اكتملت عملية Seed لجميع الكتب المتاحة.");
    }

    private async Task SeedCollectionAsync(HttpClient httpClient, (string File, string NameAr, string NameEn, string Author) collection)
    {
        string url = $"{BaseUrl}/{collection.File}";
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) return;

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var chaptersArray = root.GetProperty("chapters").EnumerateArray().ToList();
        var hadithsArray = root.GetProperty("hadiths").EnumerateArray().ToList();

        // 1. إنشاء الكتاب
        var book = new HadithBook
        {
            NameArabic = collection.NameAr,
            NameEnglish = collection.NameEn,
            Author = collection.Author
        };
        _context.HadithBooks.Add(book);
        await _context.SaveChangesAsync();

        // 2. إنشاء الأبواب
        var chapterMap = new Dictionary<int, int>();
        foreach (var chap in chaptersArray)
        {
            var chapter = new HadithChapter
            {
                NameArabic = chap.GetProperty("arabic").GetString() ?? "",
                NameEnglish = chap.GetProperty("english").GetString() ?? "",
                BookId = book.Id
            };
            _context.HadithChapters.Add(chapter);
            await _context.SaveChangesAsync();
            chapterMap[chap.GetProperty("id").GetInt32()] = chapter.Id;
        }

        // 3. إضافة الأحاديث في مجموعات
        var batch = new List<Hadith>();
        int totalSaved = 0;

        foreach (var h in hadithsArray)
        {
            try
            {
                int jsonChapterId = h.GetProperty("chapterId").GetInt32();
                if (!chapterMap.ContainsKey(jsonChapterId)) continue;

                var arabicText = h.GetProperty("arabic").GetString() ?? "";

                var hadith = new Hadith
                {
                    Number = h.GetProperty("idInBook").GetInt32(),

                    TextArabic = arabicText,

                    TextArabicSearch =
                        ArabicSearchHelper.NormalizeArabic(arabicText),

                    ChapterId = chapterMap[jsonChapterId],

                    Grade = ""
                };
                if (h.TryGetProperty("english", out var eng))
                {
                    hadith.TextEnglish = eng.TryGetProperty("text", out var t) ? t.GetString() : "";
                    hadith.Narrator = eng.TryGetProperty("narrator", out var n) ? n.GetString() : "";
                }

                if (string.IsNullOrWhiteSpace(hadith.TextArabic)) continue;

                batch.Add(hadith);

                if (batch.Count >= 200)
                {
                    await _context.Hadiths.AddRangeAsync(batch);
                    await _context.SaveChangesAsync();
                    totalSaved += batch.Count;
                    if (totalSaved % 1000 == 0) Console.WriteLine($"[PROGRESS] {collection.NameAr}: تم حفظ {totalSaved} حديث...");
                    batch.Clear();
                }
            }
            catch (Exception ex)
            {
                // تخطي الأخطاء الفردية في الأحاديث لضمان استمرار العملية
            }
        }

        if (batch.Count > 0)
        {
            await _context.Hadiths.AddRangeAsync(batch);
            await _context.SaveChangesAsync();
            totalSaved += batch.Count;
        }

        Console.WriteLine($"[DONE] تم الانتهاء من {collection.NameAr}. الإجمالي: {totalSaved} حديث.");
    }
}
