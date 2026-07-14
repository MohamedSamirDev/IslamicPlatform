using IslamicPlatform.Domain.Entites.Azkar;
using IslamicPlatform.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IslamicPlatform.Infrastructure.Data.Seeders
{
    public class AzkarSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AzkarSeeder(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            if (await _context.Azkar.AnyAsync())
            {
                _logger.LogInformation("الأذكار موجودة — تم تخطي الـ Seed");
                return;
            }

            _logger.LogInformation("بدء Seed الأذكار...");

            var azkar = new List<Zikr>();

            // ================================================================
            // ========== أذكار الصباح (Morning) — 30 ذكر ==========
            // ================================================================
            azkar.AddRange(new[]
            {
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ عَالِمَ الْغَيْبِ وَالشَّهَادَةِ فَاطِرَ السَّمَاوَاتِ وَالأَرْضِ، رَبَّ كُلِّ شَيْءٍ وَمَلِيكَهُ، أَشْهَدُ أَنْ لَا إِلَهَ إِلَّا أَنْتَ، أَعُوذُ بِكَ مِنْ شَرِّ نَفْسِي وَمِنْ شَرِّ الشَّيْطَانِ وَشِرْكِهِ",
                    TextTransliteration = "Allahumma 'alimal-ghaybi wash-shahadah",
                    Meaning = "اللهم عالم الغيب والشهادة فاطر السماوات والأرض — يقال حين يصبح",
                     Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                     Category = ZikrCategory.Morning
                },
           new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَصْبَحْتُ أُشْهِدُكَ وَأُشْهِدُ حَمَلَةَ عَرْشِكَ وَمَلَائِكَتَكَ وَجَمِيعَ خَلْقِكَ أَنَّكَ أَنْتَ اللهُ لَا إِلَهَ إِلَّا أَنْتَ وَأَنَّ مُحَمَّدًا عَبْدُكَ وَرَسُولُكَ",
    TextTransliteration = "Allahumma inni asbahtu ushhiduk",
    Meaning = "من قالها أربعًا أعتقه الله من النار",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 4,
    Category = ZikrCategory.Morning
},


new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ مِنْ فَضْلِكَ وَرَحْمَتِكَ فَإِنَّهُ لَا يَمْلِكُهَا إِلَّا أَنْتَ",
    TextTransliteration = "Allahumma inni as'aluka min fadlika wa rahmatik",
    Meaning = "اللهم إني أسألك من فضلك ورحمتك",
    Source = "مسند أحمد — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Morning
},
new Zikr
{
    TextArabic = "اللَّهُمَّ أَنْتَ خَلَقْتَنِي وَأَنْتَ تَهْدِينِي وَأَنْتَ تُطْعِمُنِي وَأَنْتَ تَسْقِينِي وَأَنْتَ تُمِيتُنِي وَأَنْتَ تُحْيِينِي",
    TextTransliteration = "Allahumma anta khalaqtani wa anta tahdini",
    Meaning = "دعاء إبراهيم عليه السلام — اللهم أنت خلقتني وأنت تهديني",
    Source = "صحيح البخاري",
    RepeatCount = 1,
    Category = ZikrCategory.Morning
},
new Zikr
{
    TextArabic = "لَا إِلَهَ إِلَّا اللهُ الْعَظِيمُ الْحَلِيمُ، لَا إِلَهَ إِلَّا اللهُ رَبُّ الْعَرْشِ الْعَظِيمِ، لَا إِلَهَ إِلَّا اللهُ رَبُّ السَّمَاوَاتِ وَرَبُّ الأَرْضِ وَرَبُّ الْعَرْشِ الْكَرِيمِ",
    TextTransliteration = "La ilaha illallahul-'Azimul-Halim",
    Meaning = "دعاء الكرب — كان النبي ﷺ يقوله عند الكرب",
    Source = "صحيح البخاري",
    RepeatCount = 1,
    Category = ZikrCategory.Morning
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الشَّيْطَانِ الرَّجِيمِ وَمِنْ هَمَزَاتِهِ وَنَفَخَاتِهِ وَنَفَثَاتِهِ",
    TextTransliteration = "Allahumma inni a'udhu bika minash-shaytanir-rajim wa min hamazatih",
    Meaning = "الاستعاذة من الشيطان وهمزاته ونفخاته ونفثاته",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Morning
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْعَافِيَةَ فِي الدُّنْيَا وَالآخِرَةِ",
    TextTransliteration = "Allahumma inni as'alukal-'afiyata fid-dunya wal-akhirah",
    Meaning = "اللهم إني أسألك العافية في الدنيا والآخرة",
    Source = "سنن ابن ماجه — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Morning
},
                new Zikr
                {
                    TextArabic = "أَعُوذُ بِاللهِ مِنَ الشَّيْطَانِ الرَّجِيمِ، اللهُ لَا إِلَهَ إِلَّا هُوَ الْحَيُّ الْقَيُّومُ لَا تَأْخُذُهُ سِنَةٌ وَلَا نَوْمٌ لَّهُ مَا فِي السَّمَاوَاتِ وَمَا فِي الْأَرْضِ مَن ذَا الَّذِي يَشْفَعُ عِندَهُ إِلَّا بِإِذْنِهِ يَعْلَمُ مَا بَيْنَ أَيْدِيهِمْ وَمَا خَلْفَهُمْ وَلَا يُحِيطُونَ بِشَيْءٍ مِّنْ عِلْمِهِ إِلَّا بِمَا شَاءَ وَسِعَ كُرْسِيُّهُ السَّمَاوَاتِ وَالْأَرْضَ وَلَا يَئُودُهُ حِفْظُهُمَا وَهُوَ الْعَلِيُّ الْعَظِيمُ",
                    TextTransliteration = "Ayat Al-Kursi",
                    Meaning = "آية الكرسي — من قرأها حين يصبح أُجير من الجن حتى يمسي",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "أَصْبَحْنَا وَأَصْبَحَ الْمُلْكُ لِلَّهِ، وَالْحَمْدُ لِلَّهِ، لَا إِلَهَ إِلَّا اللَّهُ وَحْدَهُ لَا شَرِيكَ لَهُ، لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ، رَبِّ أَسْأَلُكَ خَيْرَ مَا فِي هَذَا الْيَوْمِ وَخَيْرَ مَا بَعْدَهُ، وَأَعُوذُ بِكَ مِنْ شَرِّ مَا فِي هَذَا الْيَوْمِ وَشَرِّ مَا بَعْدَهُ",
                    TextTransliteration = "Asbahna wa asbahal mulku lillah",
                    Meaning = "أصبحنا وأصبح الملك لله",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ بِكَ أَصْبَحْنَا، وَبِكَ أَمْسَيْنَا، وَبِكَ نَحْيَا، وَبِكَ نَمُوتُ، وَإِلَيْكَ النُّشُورُ",
                    TextTransliteration = "Allahumma bika asbahna",
                    Meaning = "اللهم بك أصبحنا وبك أمسينا",
                    Source = "سنن الترمذي — حسن صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ وَبِحَمْدِهِ",
                    TextTransliteration = "Subhan Allahi wa bihamdihi",
                    Meaning = "من قالها مائة مرة حُطَّت عنه خطاياه وإن كانت مثل زبد البحر",
                    Source = "صحيح مسلم",
                    RepeatCount = 100,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "لَا إِلَهَ إِلَّا اللَّهُ وَحْدَهُ لَا شَرِيكَ لَهُ، لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ، وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ",
                    TextTransliteration = "La ilaha illa Allah wahdahu la sharika lah",
                    Meaning = "من قالها عشر مرات كان كمن أعتق أربعة أنفس من ولد إسماعيل",
                    Source = "صحيح البخاري",
                    RepeatCount = 10,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ أَنْتَ رَبِّي لَا إِلَهَ إِلَّا أَنْتَ، خَلَقْتَنِي وَأَنَا عَبْدُكَ، وَأَنَا عَلَى عَهْدِكَ وَوَعْدِكَ مَا اسْتَطَعْتُ، أَعُوذُ بِكَ مِنْ شَرِّ مَا صَنَعْتُ، أَبُوءُ لَكَ بِنِعْمَتِكَ عَلَيَّ، وَأَبُوءُ بِذَنْبِي فَاغْفِرْ لِي فَإِنَّهُ لَا يَغْفِرُ الذُّنُوبَ إِلَّا أَنْتَ",
                    TextTransliteration = "Allahumma anta Rabbi la ilaha illa ant",
                    Meaning = "سيد الاستغفار — من قاله موقنًا به فمات من يومه دخل الجنة",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْعَفْوَ وَالْعَافِيَةَ فِي الدُّنْيَا وَالآخِرَةِ، اللَّهُمَّ إِنِّي أَسْأَلُكَ الْعَفْوَ وَالْعَافِيَةَ فِي دِينِي وَدُنْيَايَ وَأَهْلِي وَمَالِي",
                    TextTransliteration = "Allahumma inni as'alukal-'afwa wal-'afiyah",
                    Meaning = "اللهم إني أسألك العفو والعافية في الدنيا والآخرة",
                    Source = "سنن ابن ماجه — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْهَمِّ وَالْحَزَنِ، وَالْعَجْزِ وَالْكَسَلِ، وَالْبُخْلِ وَالْجُبْنِ، وَضَلَعِ الدَّيْنِ وَغَلَبَةِ الرِّجَالِ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-hammi wal-hazan",
                    Meaning = "اللهم إني أعوذ بك من الهم والحزن والعجز والكسل",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ مَا أَصْبَحَ بِي مِنْ نِعْمَةٍ أَوْ بِأَحَدٍ مِنْ خَلْقِكَ فَمِنْكَ وَحْدَكَ لَا شَرِيكَ لَكَ، فَلَكَ الْحَمْدُ وَلَكَ الشُّكْرُ",
                    TextTransliteration = "Allahumma ma asbaha bi min ni'mah",
                    Meaning = "من قالها حين يصبح فقد أدَّى شكر يومه",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "حَسْبِيَ اللهُ لَا إِلَهَ إِلَّا هُوَ عَلَيْهِ تَوَكَّلْتُ وَهُوَ رَبُّ الْعَرْشِ الْعَظِيمِ",
                    TextTransliteration = "Hasbiyallahu la ilaha illa huwa",
                    Meaning = "كفاني الله لا إله إلا هو عليه توكلت",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 7,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "بِسْمِ اللهِ الَّذِي لَا يَضُرُّ مَعَ اسْمِهِ شَيْءٌ فِي الْأَرْضِ وَلَا فِي السَّمَاءِ وَهُوَ السَّمِيعُ الْعَلِيمُ",
                    TextTransliteration = "Bismillahil-ladhi la yadurru ma'asmihi shay'",
                    Meaning = "من قالها ثلاثًا لم تصبه فجأة بلاء حتى يمسي",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "رَضِيتُ بِاللهِ رَبًّا، وَبِالإِسْلَامِ دِينًا، وَبِمُحَمَّدٍ صَلَّى اللهُ عَلَيْهِ وَسَلَّمَ نَبِيًّا",
                    TextTransliteration = "Raditu billahi Rabba wa bil-Islami dinan",
                    Meaning = "من قالها ثلاثًا كان حقًا على الله أن يُرضيه يوم القيامة",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "قُلْ هُوَ اللهُ أَحَدٌ، اللهُ الصَّمَدُ، لَمْ يَلِدْ وَلَمْ يُولَدْ، وَلَمْ يَكُنْ لَهُ كُفُوًا أَحَدٌ",
                    TextTransliteration = "Qul Huwa Allahu Ahad (Al-Ikhlas)",
                    Meaning = "سورة الإخلاص — تعدل ثلث القرآن",
                    Source = " أبو داود والترمذي",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "قُلْ أَعُوذُ بِرَبِّ الْفَلَقِ، مِنْ شَرِّ مَا خَلَقَ، وَمِنْ شَرِّ غَاسِقٍ إِذَا وَقَبَ، وَمِنْ شَرِّ النَّفَّاثَاتِ فِي الْعُقَدِ، وَمِنْ شَرِّ حَاسِدٍ إِذَا حَسَدَ",
                    TextTransliteration = "Qul A'udhu bi Rabb il-Falaq (Al-Falaq)",
                    Meaning = "سورة الفلق — للحفظ من شر الخلق والحسد والسحر",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "قُلْ أَعُوذُ بِرَبِّ النَّاسِ، مَلِكِ النَّاسِ، إِلَهِ النَّاسِ، مِنْ شَرِّ الْوَسْوَاسِ الْخَنَّاسِ، الَّذِي يُوَسْوِسُ فِي صُدُورِ النَّاسِ، مِنَ الْجِنَّةِ وَالنَّاسِ",
                    TextTransliteration = "Qul A'udhu bi Rabb in-Nas (An-Nas)",
                    Meaning = "سورة الناس — للحفظ من الوسواس والشيطان",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ عَافِنِي فِي بَدَنِي، اللَّهُمَّ عَافِنِي فِي سَمْعِي، اللَّهُمَّ عَافِنِي فِي بَصَرِي، لَا إِلَهَ إِلَّا أَنْتَ",
                    TextTransliteration = "Allahumma 'afini fi badani",
                    Meaning = "اللهم عافني في بدني وسمعي وبصري",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْكُفْرِ وَالْفَقْرِ، وَأَعُوذُ بِكَ مِنْ عَذَابِ الْقَبْرِ، لَا إِلَهَ إِلَّا أَنْتَ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-kufri wal-faqr",
                    Meaning = "اللهم إني أعوذ بك من الكفر والفقر وعذاب القبر",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ عِلْمًا نَافِعًا، وَرِزْقًا طَيِّبًا، وَعَمَلًا مُتَقَبَّلًا",
                    TextTransliteration = "Allahumma inni as'aluka 'ilman nafi'an",
                    Meaning = "اللهم إني أسألك علمًا نافعًا ورزقًا طيبًا وعملًا متقبلًا",
                    Source = "سنن ابن ماجه — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ وَبِحَمْدِهِ عَدَدَ خَلْقِهِ، وَرِضَا نَفْسِهِ، وَزِنَةَ عَرْشِهِ، وَمِدَادَ كَلِمَاتِهِ",
                    TextTransliteration = "Subhan Allahi wa bihamdihi 'adada khalqih",
                    Meaning = "سبحان الله وبحمده عدد خلقه — ثلاث كلمات خفيفات ثقيلات في الميزان",
                    Source = "صحيح مسلم",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْجَنَّةَ وَأَعُوذُ بِكَ مِنَ النَّارِ",
                    TextTransliteration = "Allahumma inni as'alukal-jannah",
                    Meaning = "من قالها ثلاثًا سألت الجنة ربها أن يدخله إياها",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ صَلِّ وَسَلِّمْ عَلَى نَبِيِّنَا مُحَمَّدٍ",
                    TextTransliteration = "Allahumma salli wa sallim 'ala nabiyyina Muhammad",
                    Meaning = "من صلى عليَّ صلاةً صلى الله عليه بها عشرًا",
                    Source = "صحيح مسلم",
                    RepeatCount = 10,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "أَصْبَحْنَا عَلَى فِطْرَةِ الإِسْلَامِ، وَعَلَى كَلِمَةِ الإِخْلَاصِ، وَعَلَى دِينِ نَبِيِّنَا مُحَمَّدٍ صَلَّى اللهُ عَلَيْهِ وَسَلَّمَ، وَعَلَى مِلَّةِ أَبِينَا إِبْرَاهِيمَ حَنِيفًا مُسْلِمًا وَمَا كَانَ مِنَ الْمُشْرِكِينَ",
                    TextTransliteration = "Asbahna 'ala fitratil-Islam",
                    Meaning = "أصبحنا على فطرة الإسلام وعلى كلمة الإخلاص",
                    Source = "مسند أحمد — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "يَا حَيُّ يَا قَيُّومُ بِرَحْمَتِكَ أَسْتَغِيثُ، أَصْلِحْ لِي شَأْنِي كُلَّهُ وَلَا تَكِلْنِي إِلَى نَفْسِي طَرْفَةَ عَيْنٍ",
                    TextTransliteration = "Ya Hayyu ya Qayyum birahmatika astaghith",
                    Meaning = "يا حي يا قيوم برحمتك أستغيث أصلح لي شأني كله",
                    Source = "مستدرك الحاكم — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ مِنْ خَيْرِ هَذَا الْيَوْمِ فَتْحَهُ وَنَصْرَهُ وَنُورَهُ وَبَرَكَتَهُ وَهُدَاهُ، وَأَعُوذُ بِكَ مِنْ شَرِّ مَا فِيهِ وَشَرِّ مَا بَعْدَهُ",
                    TextTransliteration = "Allahumma inni as'aluka min khayri haadhal-yawm",
                    Meaning = "اللهم إني أسألك من خير هذا اليوم فتحه ونصره ونوره وبركته وهداه",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "لَا إِلَهَ إِلَّا اللهُ وَحْدَهُ لَا شَرِيكَ لَهُ، لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ يُحْيِي وَيُمِيتُ وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ",
                    TextTransliteration = "La ilaha ill-Allah wahdahu la sharika lahu, lahul-mulk",
                    Meaning = "لا إله إلا الله وحده لا شريك له — من قالها مئة مرة في اليوم",
                    Source = "صحيح البخاري",
                    RepeatCount = 100,
                    Category = ZikrCategory.Morning
                },
               
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْبَرَصِ وَالْجُنُونِ وَالْجُذَامِ وَمِنْ سَيِّئِ الأَسْقَامِ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-baras wal-junun",
                    Meaning = "اللهم إني أعوذ بك من البرص والجنون والجذام وسيئ الأسقام",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الثَّبَاتَ فِي الأَمْرِ، وَالْعَزِيمَةَ عَلَى الرُّشْدِ، وَأَسْأَلُكَ شُكْرَ نِعْمَتِكَ، وَحُسْنَ عِبَادَتِكَ",
                    TextTransliteration = "Allahumma inni as'alukat-thabata fil-amr",
                    Meaning = "اللهم إني أسألك الثبات في الأمر والعزيمة على الرشد",
                    Source = "مسند أحمد — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Morning
                },
               
                new Zikr
                {
                    TextArabic = "اسْتَغْفِرُ اللهَ وَأَتُوبُ إِلَيْهِ",
                    TextTransliteration = "Astaghfirullaha wa atubu ilayh",
                    Meaning = "من قال ذلك مئة مرة في اليوم غُفرت له ذنوبه وإن كانت مثل زبد البحر",
                    Source = "صحيح البخاري",
                    RepeatCount = 100,
                    Category = ZikrCategory.Morning
                },
            });

            // ================================================================
            // ========== أذكار المساء (Evening) — 28 ذكر ==========
            // ================================================================
            azkar.AddRange(new[]
            {
                new Zikr
                {
                    TextArabic = "أَعُوذُ بِاللهِ مِنَ الشَّيْطَانِ الرَّجِيمِ، اللهُ لَا إِلَهَ إِلَّا هُوَ الْحَيُّ الْقَيُّومُ لَا تَأْخُذُهُ سِنَةٌ وَلَا نَوْمٌ لَّهُ مَا فِي السَّمَاوَاتِ وَمَا فِي الْأَرْضِ مَن ذَا الَّذِي يَشْفَعُ عِندَهُ إِلَّا بِإِذْنِهِ يَعْلَمُ مَا بَيْنَ أَيْدِيهِمْ وَمَا خَلْفَهُمْ وَلَا يُحِيطُونَ بِشَيْءٍ مِّنْ عِلْمِهِ إِلَّا بِمَا شَاءَ وَسِعَ كُرْسِيُّهُ السَّمَاوَاتِ وَالْأَرْضَ وَلَا يَئُودُهُ حِفْظُهُمَا وَهُوَ الْعَلِيُّ الْعَظِيمُ",
                    TextTransliteration = "Ayat Al-Kursi — Evening",
                    Meaning = "آية الكرسي — من قرأها حين يمسي أُجير من الجن حتى يصبح",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "أَمْسَيْنَا وَأَمْسَى الْمُلْكُ لِلَّهِ، وَالْحَمْدُ لِلَّهِ، لَا إِلَهَ إِلَّا اللَّهُ وَحْدَهُ لَا شَرِيكَ لَهُ، لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ، رَبِّ أَسْأَلُكَ خَيْرَ مَا فِي هَذِهِ اللَّيْلَةِ وَخَيْرَ مَا بَعْدَهَا، وَأَعُوذُ بِكَ مِنْ شَرِّ مَا فِي هَذِهِ اللَّيْلَةِ وَشَرِّ مَا بَعْدَهَا",
                    TextTransliteration = "Amsayna wa amsal mulku lillah",
                    Meaning = "أمسينا وأمسى الملك لله",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ بِكَ أَمْسَيْنَا، وَبِكَ أَصْبَحْنَا، وَبِكَ نَحْيَا، وَبِكَ نَمُوتُ، وَإِلَيْكَ الْمَصِيرُ",
                    TextTransliteration = "Allahumma bika amsayna",
                    Meaning = "اللهم بك أمسينا وبك أصبحنا",
                    Source = "سنن الترمذي — حسن صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "أَعُوذُ بِكَلِمَاتِ اللهِ التَّامَّاتِ مِنْ شَرِّ مَا خَلَقَ",
                    TextTransliteration = "A'udhu bikalimatillahit-tammati min sharri ma khalaq",
                    Meaning = "من قالها ثلاثًا لم تضره حُمة تلك الليلة",
                    Source = "صحيح مسلم",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ أَنْتَ رَبِّي لَا إِلَهَ إِلَّا أَنْتَ، خَلَقْتَنِي وَأَنَا عَبْدُكَ، وَأَنَا عَلَى عَهْدِكَ وَوَعْدِكَ مَا اسْتَطَعْتُ، أَعُوذُ بِكَ مِنْ شَرِّ مَا صَنَعْتُ، أَبُوءُ لَكَ بِنِعْمَتِكَ عَلَيَّ، وَأَبُوءُ بِذَنْبِي فَاغْفِرْ لِي فَإِنَّهُ لَا يَغْفِرُ الذُّنُوبَ إِلَّا أَنْتَ",
                    TextTransliteration = "Sayyidul-Istighfar — Evening",
                    Meaning = "سيد الاستغفار — من قاله موقنًا به فمات من ليلته دخل الجنة",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ مَا أَمْسَى بِي مِنْ نِعْمَةٍ أَوْ بِأَحَدٍ مِنْ خَلْقِكَ فَمِنْكَ وَحْدَكَ لَا شَرِيكَ لَكَ، فَلَكَ الْحَمْدُ وَلَكَ الشُّكْرُ",
                    TextTransliteration = "Allahumma ma amsa bi min ni'mah",
                    Meaning = "من قالها حين يمسي فقد أدَّى شكر ليلته",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "بِسْمِ اللهِ الَّذِي لَا يَضُرُّ مَعَ اسْمِهِ شَيْءٌ فِي الْأَرْضِ وَلَا فِي السَّمَاءِ وَهُوَ السَّمِيعُ الْعَلِيمُ",
                    TextTransliteration = "Bismillahil-ladhi la yadurru ma'asmihi shay' — Evening",
                    Meaning = "من قالها ثلاثًا لم تصبه فجأة بلاء حتى يصبح",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ وَبِحَمْدِهِ",
                    TextTransliteration = "Subhan Allahi wa bihamdihi — Evening",
                    Meaning = "سبحان الله وبحمده — من قالها مائة مرة",
                    Source = "صحيح مسلم",
                    RepeatCount = 100,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "رَضِيتُ بِاللهِ رَبًّا، وَبِالإِسْلَامِ دِينًا، وَبِمُحَمَّدٍ صَلَّى اللهُ عَلَيْهِ وَسَلَّمَ نَبِيًّا",
                    TextTransliteration = "Raditu billahi Rabba — Evening",
                    Meaning = "من قالها ثلاثًا كان حقًا على الله أن يُرضيه",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "حَسْبِيَ اللهُ لَا إِلَهَ إِلَّا هُوَ عَلَيْهِ تَوَكَّلْتُ وَهُوَ رَبُّ الْعَرْشِ الْعَظِيمِ",
                    TextTransliteration = "Hasbiyallahu la ilaha illa huwa — Evening",
                    Meaning = "من قالها سبعًا كفاه الله ما أهمه",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 7,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "قُلْ هُوَ اللهُ أَحَدٌ، اللهُ الصَّمَدُ، لَمْ يَلِدْ وَلَمْ يُولَدْ، وَلَمْ يَكُنْ لَهُ كُفُوًا أَحَدٌ",
                    TextTransliteration = "Qul Huwa Allahu Ahad — Evening",
                    Meaning = "سورة الإخلاص",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "قُلْ أَعُوذُ بِرَبِّ الْفَلَقِ، مِنْ شَرِّ مَا خَلَقَ، وَمِنْ شَرِّ غَاسِقٍ إِذَا وَقَبَ، وَمِنْ شَرِّ النَّفَّاثَاتِ فِي الْعُقَدِ، وَمِنْ شَرِّ حَاسِدٍ إِذَا حَسَدَ",
                    TextTransliteration = "Al-Falaq — Evening",
                    Meaning = "سورة الفلق",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "قُلْ أَعُوذُ بِرَبِّ النَّاسِ، مَلِكِ النَّاسِ، إِلَهِ النَّاسِ، مِنْ شَرِّ الْوَسْوَاسِ الْخَنَّاسِ، الَّذِي يُوَسْوِسُ فِي صُدُورِ النَّاسِ، مِنَ الْجِنَّةِ وَالنَّاسِ",
                    TextTransliteration = "An-Nas — Evening",
                    Meaning = "سورة الناس",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ عَافِنِي فِي بَدَنِي، اللَّهُمَّ عَافِنِي فِي سَمْعِي، اللَّهُمَّ عَافِنِي فِي بَصَرِي، لَا إِلَهَ إِلَّا أَنْتَ",
                    TextTransliteration = "Allahumma 'afini fi badani — Evening",
                    Meaning = "اللهم عافني في بدني وسمعي وبصري",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْكُفْرِ وَالْفَقْرِ، وَأَعُوذُ بِكَ مِنْ عَذَابِ الْقَبْرِ، لَا إِلَهَ إِلَّا أَنْتَ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-kufri wal-faqr — Evening",
                    Meaning = "اللهم إني أعوذ بك من الكفر والفقر وعذاب القبر",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ فَاطِرَ السَّمَاوَاتِ وَالأَرْضِ، عَالِمَ الْغَيْبِ وَالشَّهَادَةِ، رَبَّ كُلِّ شَيْءٍ وَمَلِيكَهُ، أَشْهَدُ أَنْ لَا إِلَهَ إِلَّا أَنْتَ، أَعُوذُ بِكَ مِنْ شَرِّ نَفْسِي وَشَرِّ الشَّيْطَانِ وَشِرْكِهِ",
                    TextTransliteration = "Allahumma fatiras-samawati wal-ard",
                    Meaning = "اللهم فاطر السماوات والأرض عالم الغيب والشهادة",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "يَا حَيُّ يَا قَيُّومُ بِرَحْمَتِكَ أَسْتَغِيثُ، أَصْلِحْ لِي شَأْنِي كُلَّهُ وَلَا تَكِلْنِي إِلَى نَفْسِي طَرْفَةَ عَيْنٍ",
                    TextTransliteration = "Ya Hayyu ya Qayyum birahmatika astaghith — Evening",
                    Meaning = "يا حي يا قيوم برحمتك أستغيث",
                    Source = "مستدرك الحاكم — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْعَفْوَ وَالْعَافِيَةَ فِي الدُّنْيَا وَالآخِرَةِ",
                    TextTransliteration = "Allahumma inni as'alukal-'afwa wal-'afiyah — Evening",
                    Meaning = "اللهم إني أسألك العفو والعافية في الدنيا والآخرة",
                    Source = "سنن ابن ماجه — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "أَمْسَيْنَا عَلَى فِطْرَةِ الإِسْلَامِ، وَعَلَى كَلِمَةِ الإِخْلَاصِ، وَعَلَى دِينِ نَبِيِّنَا مُحَمَّدٍ صَلَّى اللهُ عَلَيْهِ وَسَلَّمَ، وَعَلَى مِلَّةِ أَبِينَا إِبْرَاهِيمَ حَنِيفًا مُسْلِمًا وَمَا كَانَ مِنَ الْمُشْرِكِينَ",
                    TextTransliteration = "Amsayna 'ala fitratil-Islam",
                    Meaning = "أمسينا على فطرة الإسلام وعلى كلمة الإخلاص",
                    Source = "مسند أحمد — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَمْسَيْتُ أُشْهِدُكَ وَأُشْهِدُ حَمَلَةَ عَرْشِكَ وَمَلَائِكَتَكَ وَجَمِيعَ خَلْقِكَ أَنَّكَ أَنْتَ اللهُ لَا إِلَهَ إِلَّا أَنْتَ وَأَنَّ مُحَمَّدًا عَبْدُكَ وَرَسُولُكَ",
                    TextTransliteration = "Allahumma inni amsaytu ushhiduk",
                    Meaning = "من قالها أربعًا أعتقه الله من النار",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 4,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْهَمِّ وَالْحَزَنِ، وَالْعَجْزِ وَالْكَسَلِ، وَالْبُخْلِ وَالْجُبْنِ، وَضَلَعِ الدَّيْنِ وَغَلَبَةِ الرِّجَالِ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-hammi — Evening",
                    Meaning = "اللهم إني أعوذ بك من الهم والحزن",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اسْتَغْفِرُ اللهَ وَأَتُوبُ إِلَيْهِ",
                    TextTransliteration = "Astaghfirullaha wa atubu ilayh — Evening",
                    Meaning = "الاستغفار مئة مرة",
                    Source = "صحيح البخاري",
                    RepeatCount = 100,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ صَلِّ وَسَلِّمْ عَلَى نَبِيِّنَا مُحَمَّدٍ",
                    TextTransliteration = "Allahumma salli wa sallim 'ala nabiyyina Muhammad — Evening",
                    Meaning = "الصلاة على النبي عشر مرات",
                    Source = "صحيح مسلم",
                    RepeatCount = 10,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ وَبِحَمْدِهِ عَدَدَ خَلْقِهِ، وَرِضَا نَفْسِهِ، وَزِنَةَ عَرْشِهِ، وَمِدَادَ كَلِمَاتِهِ",
                    TextTransliteration = "Subhan Allahi wa bihamdihi 'adada khalqih — Evening",
                    Meaning = "ثلاث كلمات خفيفات ثقيلات في الميزان",
                    Source = "صحيح مسلم",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْجَنَّةَ وَأَعُوذُ بِكَ مِنَ النَّارِ",
                    TextTransliteration = "Allahumma inni as'alukal-jannah — Evening",
                    Meaning = "سألت الجنة ربها أن يدخله إياها",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْبَرَصِ وَالْجُنُونِ وَالْجُذَامِ وَمِنْ سَيِّئِ الأَسْقَامِ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-baras — Evening",
                    Meaning = "الاستعاذة من البرص والجنون والجذام",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ اغْفِرْ لِي وَلِوَالِدَيَّ وَلِلْمُؤْمِنِينَ يَوْمَ يَقُومُ الْحِسَابُ",
                    TextTransliteration = "Allahummaghfir li wa liwalidayya wal-mu'minin",
                    Meaning = "اللهم اغفر لي ولوالدي وللمؤمنين يوم يقوم الحساب",
                    Source = "القرآن الكريم — إبراهيم:41",
                    RepeatCount = 1,
                    Category = ZikrCategory.Evening
                },new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ خَيْرَ هَذِهِ اللَّيْلَةِ وَخَيْرَ مَا فِيهَا وَأَعُوذُ بِكَ مِنْ شَرِّ هَذِهِ اللَّيْلَةِ وَشَرِّ مَا فِيهَا",
    TextTransliteration = "Allahumma inni as'aluka khayra hadhihil-laylah",
    Meaning = "اللهم إني أسألك خير هذه الليلة وخير ما فيها",
    Source = "صحيح مسلم",
    RepeatCount = 1,
    Category = ZikrCategory.Evening
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الثَّبَاتَ فِي الأَمْرِ وَالْعَزِيمَةَ عَلَى الرُّشْدِ وَأَسْأَلُكَ شُكْرَ نِعْمَتِكَ وَحُسْنَ عِبَادَتِكَ",
    TextTransliteration = "Allahumma inni as'alukat-thabata fil-amr — Evening",
    Meaning = "اللهم إني أسألك الثبات في الأمر والعزيمة على الرشد — مساءً",
    Source = "مسند أحمد — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Evening
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْجَنَّةَ وَمَا قَرَّبَ إِلَيْهَا مِنْ قَوْلٍ أَوْ عَمَلٍ وَأَعُوذُ بِكَ مِنَ النَّارِ وَمَا قَرَّبَ إِلَيْهَا مِنْ قَوْلٍ أَوْ عَمَلٍ",
    TextTransliteration = "Allahumma inni as'alukal-jannata wa ma qarraba ilayhah",
    Meaning = "اللهم إني أسألك الجنة وما قرب إليها من قول أو عمل",
    Source = "سنن ابن ماجه — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Evening
},
new Zikr
{
    TextArabic = "اللَّهُمَّ اسْتُرْ عَوْرَاتِي وَآمِنْ رَوْعَاتِي",
    TextTransliteration = "Allahumma stur 'awrati wa amin raw'ati",
    Meaning = "اللهم استر عوراتي وآمن روعاتي",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Evening
},
new Zikr
{
    TextArabic = "اللَّهُمَّ احْفَظْنِي مِنْ بَيْنِ يَدَيَّ وَمِنْ خَلْفِي وَعَنْ يَمِينِي وَعَنْ شِمَالِي وَمِنْ فَوْقِي وَأَعُوذُ بِعَظَمَتِكَ أَنْ أُغْتَالَ مِنْ تَحْتِي",
    TextTransliteration = "Allahumma ihfazni min bayni yadayya wa min khalfi",
    Meaning = "اللهم احفظني من بين يديَّ ومن خلفي وعن يميني وعن شمالي",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Evening
},
new Zikr
{
    TextArabic = "اللَّهُمَّ عَالِمَ الْغَيْبِ وَالشَّهَادَةِ فَاطِرَ السَّمَاوَاتِ وَالأَرْضِ رَبَّ كُلِّ شَيْءٍ وَمَلِيكَهُ أَشْهَدُ أَنْ لَا إِلَهَ إِلَّا أَنْتَ أَعُوذُ بِكَ مِنْ شَرِّ نَفْسِي وَمِنْ شَرِّ الشَّيْطَانِ وَشِرْكِهِ",
    TextTransliteration = "Allahumma 'alimal-ghaybi wash-shahadah — Evening",
    Meaning = "اللهم عالم الغيب والشهادة فاطر السماوات والأرض — يقال حين يمسي",
    Source = "سنن الترمذي — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Evening
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الشَّيْطَانِ الرَّجِيمِ وَمِنْ هَمَزَاتِهِ وَنَفَخَاتِهِ وَنَفَثَاتِهِ",
    TextTransliteration = "Allahumma inni a'udhu bika minash-shaytanir-rajim — Evening",
    Meaning = "الاستعاذة من الشيطان وهمزاته — مساءً",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.Evening
},
            });

            // ================================================================
            // ========== أذكار النوم (Sleep) — 30 ذكر ==========
            // ================================================================
            azkar.AddRange(new[]
            {
                new Zikr
                {
                    TextArabic = "بِاسْمِكَ اللَّهُمَّ أَمُوتُ وَأَحْيَا",
                    TextTransliteration = "Bismika Allahumma amutu wa ahya",
                    Meaning = "باسمك اللهم أموت وأحيا",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ قِنِي عَذَابَكَ يَوْمَ تَبْعَثُ عِبَادَكَ",
                    TextTransliteration = "Allahumma qini adhabaka yawma tab'athu ibadak",
                    Meaning = "اللهم قني عذابك يوم تبعث عبادك",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ",
                    TextTransliteration = "Subhan Allah",
                    Meaning = "تسبيح قبل النوم ثلاثًا وثلاثين",
                    Source = "صحيح البخاري",
                    RepeatCount = 33,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "الْحَمْدُ لِلَّهِ",
                    TextTransliteration = "Alhamdulillah",
                    Meaning = "تحميد قبل النوم ثلاثًا وثلاثين",
                    Source = "صحيح البخاري",
                    RepeatCount = 33,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "اللهُ أَكْبَرُ",
                    TextTransliteration = "Allahu Akbar",
                    Meaning = "تكبير قبل النوم أربعًا وثلاثين",
                    Source = "صحيح البخاري",
                    RepeatCount = 34,
                    Category = ZikrCategory.Sleep
                },

                new Zikr
                {
                    TextArabic = "اللَّهُمَّ أَسْلَمْتُ نَفْسِي إِلَيْكَ، وَفَوَّضْتُ أَمْرِي إِلَيْكَ، وَوَجَّهْتُ وَجْهِي إِلَيْكَ، وَأَلْجَأْتُ ظَهْرِي إِلَيْكَ، رَغْبَةً وَرَهْبَةً إِلَيْكَ، لَا مَلْجَأَ وَلَا مَنْجَا مِنْكَ إِلَّا إِلَيْكَ، آمَنْتُ بِكِتَابِكَ الَّذِي أَنْزَلْتَ وَبِنَبِيِّكَ الَّذِي أَرْسَلْتَ",
                    TextTransliteration = "Allahumma aslamtu nafsi ilayk",
                    Meaning = "من قالها ثم مات من ليلته مات على الفطرة",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ خَلَقْتَ نَفْسِي وَأَنْتَ تَتَوَفَّاهَا، لَكَ مَمَاتُهَا وَمَحْيَاهَا، إِنْ أَحْيَيْتَهَا فَاحْفَظْهَا، وَإِنْ أَمَتَّهَا فَاغْفِرْ لَهَا",
                    TextTransliteration = "Allahumma khalaqta nafsi wa anta tatawaffaha",
                    Meaning = "اللهم خلقت نفسي وأنت تتوفاها لك مماتها ومحياها",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "بِاسْمِكَ رَبِّي وَضَعْتُ جَنْبِي، وَبِكَ أَرْفَعُهُ، فَإِنْ أَمْسَكْتَ نَفْسِي فَارْحَمْهَا، وَإِنْ أَرْسَلْتَهَا فَاحْفَظْهَا بِمَا تَحْفَظُ بِهِ عِبَادَكَ الصَّالِحِينَ",
                    TextTransliteration = "Bismika Rabbi wada'tu janbi",
                    Meaning = "باسمك ربي وضعت جنبي وبك أرفعه",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
               new Zikr
               {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْعَافِيَةَ",
                    TextTransliteration = "Allahumma inni as'alukal-'afiyah",
                    Meaning = "اللهم إني أسألك العافية",
                    Source = "صحيح مسلم 2713",
                    RepeatCount = 1,
                   Category = ZikrCategory.Sleep
               },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ رَبَّ السَّمَاوَاتِ وَرَبَّ الأَرْضِ وَرَبَّ الْعَرْشِ الْعَظِيمِ، رَبَّنَا وَرَبَّ كُلِّ شَيْءٍ، فَالِقَ الْحَبِّ وَالنَّوَى، وَمُنْزِلَ التَّوْرَاةِ وَالإِنْجِيلِ وَالْقُرْآنِ، أَعُوذُ بِكَ مِنْ شَرِّ كُلِّ شَيْءٍ أَنْتَ آخِذٌ بِنَاصِيَتِهِ",
                    TextTransliteration = "Allahumma Rabbas-samawati wa Rabbal-ard",
                    Meaning = "اللهم رب السماوات ورب الأرض ورب العرش العظيم",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الشَّيْطَانِ وَهَمَزَاتِهِ وَأَعُوذُ بِكَ رَبِّ أَنْ يَحْضُرُونِ",
                    TextTransliteration = "Allahumma inni a'udhu bika minash-shaytan",
                    Meaning = "اللهم إني أعوذ بك من الشيطان وهمزاته",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "قُلْ هُوَ اللهُ أَحَدٌ، اللهُ الصَّمَدُ، لَمْ يَلِدْ وَلَمْ يُولَدْ، وَلَمْ يَكُنْ لَهُ كُفُوًا أَحَدٌ",
                    TextTransliteration = "Al-Ikhlas before sleep",
                    Meaning = "سورة الإخلاص قبل النوم",
                    Source = "صحيح البخاري",
                    RepeatCount = 3,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "قُلْ أَعُوذُ بِرَبِّ الْفَلَقِ",
                    TextTransliteration = "Al-Falaq before sleep",
                    Meaning = "سورة الفلق — يمسح بها على جسده",
                    Source = "صحيح البخاري",
                    RepeatCount = 3,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "قُلْ أَعُوذُ بِرَبِّ النَّاسِ",
                    TextTransliteration = "An-Nas before sleep",
                    Meaning = "سورة الناس — يمسح بها على جسده",
                    Source = "صحيح البخاري",
                    RepeatCount = 3,
                    Category = ZikrCategory.Sleep
                },
               
               
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ أَنْتَ الأَوَّلُ فَلَيْسَ قَبْلَكَ شَيْءٌ وَأَنْتَ الآخِرُ فَلَيْسَ بَعْدَكَ شَيْءٌ وَأَنْتَ الظَّاهِرُ فَلَيْسَ فَوْقَكَ شَيْءٌ وَأَنْتَ الْبَاطِنُ فَلَيْسَ دُونَكَ شَيْءٌ اقْضِ عَنَّا الدَّيْنَ وَأَغْنِنَا مِنَ الْفَقْرِ",
                    TextTransliteration = "Allahumma antal-Awwal",
                    Meaning = "اللهم أنت الأول فليس قبلك شيء — دعاء النبي ﷺ حين يأوي إلى فراشه",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "الْحَمْدُ لِلَّهِ الَّذِي أَطْعَمَنَا وَسَقَانَا وَكَفَانَا وَآوَانَا، فَكَمْ مِمَّنْ لَا كَافِيَ لَهُ وَلَا مُؤْوِيَ",
                    TextTransliteration = "Alhamdulillah alladhi at'amana wa saqana",
                    Meaning = "الحمد لله الذي أطعمنا وسقانا وكفانا وآوانا",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ أَنِّي أَعُوذُ بِوَجْهِكَ الْكَرِيمِ وَكَلِمَاتِكَ التَّامَّاتِ مِنْ شَرِّ مَا أَنْتَ آخِذٌ بِنَاصِيَتِهِ",
                    TextTransliteration = "Allahumma inni a'udhu biwajhikal-karim",
                    Meaning = "اللهم إني أعوذ بوجهك الكريم وكلماتك التامات",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
                new Zikr
                {
                    TextArabic = "بِاسْمِ اللهِ وَضَعْتُ جَنْبِي اللَّهُمَّ اغْفِرْ لِي ذَنْبِي ",
                    TextTransliteration = "Bismillah wada'tu janbi Allahummaghfir li dhanbi",
                    Meaning = "باسم الله وضعت جنبي اللهم اغفر لي ذنبي",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Sleep
                },
               
                new Zikr
                {
                    TextArabic = "رَبِّ قِنِي عَذَابَكَ يَوْمَ تَجْمَعُ عِبَادَكَ",
                    TextTransliteration = "Rabbi qini adhabaka yawma tajma'u ibadak",
                    Meaning = "ربِّ قني عذابك يوم تجمع عبادك",
                    Source = "صحيح مسلم",
                    RepeatCount = 3,
                    Category = ZikrCategory.Sleep
                },
               
            });

            // ================================================================
            // ========== أذكار الاستيقاظ (WakeUp) — 20 ذكر ==========
            // ================================================================
            azkar.AddRange(new[]
            {
                new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ خَيْرَ هَذَا الْيَوْمِ وَخَيْرَ مَا فِيهِ وَأَعُوذُ بِكَ مِنْ شَرِّهِ وَشَرِّ مَا فِيهِ",
    TextTransliteration = "Allahumma inni as'aluka khayra hadhal-yawm",
    Meaning = "اللهم إني أسألك خير هذا اليوم وخير ما فيه",
    Source = "صحيح مسلم",
    RepeatCount = 1,
    Category = ZikrCategory.WakeUp
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَصْبَحْتُ مِنْكَ فِي نِعْمَةٍ وَعَافِيَةٍ وَسِتْرٍ فَأَتِمَّ عَلَيَّ نِعْمَتَكَ وَعَافِيَتَكَ وَسِتْرَكَ فِي الدُّنْيَا وَالآخِرَةِ",
    TextTransliteration = "Allahumma inni asbahtu minka fi ni'mah wa 'afiyah wa sitr",
    Meaning = "اللهم إني أصبحت منك في نعمة وعافية وستر",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.WakeUp
},
new Zikr
{
    TextArabic = "الْحَمْدُ لِلَّهِ الَّذِي رَدَّ عَلَيَّ رُوحِي وَعَافَانِي فِي جَسَدِي وَأَذِنَ لِي بِذِكْرِهِ",
    TextTransliteration = "Alhamdulillahil-ladhi radda 'alayya ruhi wa 'afani fi jasadi",
    Meaning = "الحمد لله الذي رد عليَّ روحي وعافاني في جسدي",
    Source = "سنن الترمذي — حسن",
    RepeatCount = 1,
    Category = ZikrCategory.WakeUp
},
                new Zikr
                {
                    TextArabic = "الْحَمْدُ لِلَّهِ الَّذِي أَحْيَانَا بَعْدَ مَا أَمَاتَنَا وَإِلَيْهِ النُّشُورُ",
                    TextTransliteration = "Alhamdu lillahil-ladhi ahyana ba'da ma amatana wa ilayhin-nushur",
                    Meaning = "الحمد لله الذي أحيانا بعد ما أماتنا وإليه النشور",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
                new Zikr
                {
                    TextArabic = "لَا إِلَهَ إِلَّا اللهُ وَحْدَهُ لَا شَرِيكَ لَهُ لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ سُبْحَانَ اللهِ وَالْحَمْدُ لِلَّهِ وَلَا إِلَهَ إِلَّا اللهُ وَاللهُ أَكْبَرُ وَلَا حَوْلَ وَلَا قُوَّةَ إِلَّا بِاللهِ الْعَلِيِّ الْعَظِيمِ",
                    TextTransliteration = "La ilaha illa Allah wahdahu — full dhikr upon waking",
                    Meaning = "من قالها عند الاستيقاظ ثم دعا غُفر له",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ لَكَ الْحَمْدُ أَنْتَ قَيِّمُ السَّمَاوَاتِ وَالأَرْضِ وَمَنْ فِيهِنَّ، وَلَكَ الْحَمْدُ لَكَ مُلْكُ السَّمَاوَاتِ وَالأَرْضِ وَمَنْ فِيهِنَّ",
                    TextTransliteration = "Allahumma lakal-hamd anta qayyimus-samawat",
                    Meaning = "اللهم لك الحمد أنت قيم السماوات والأرض ومن فيهن",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
                new Zikr
                {
                    TextArabic = "الْحَمْدُ لِلَّهِ الَّذِي عَافَانِي فِي جَسَدِي وَرَدَّ عَلَيَّ رُوحِي وَأَذِنَ لِي بِذِكْرِهِ",
                    TextTransliteration = "Alhamdulillahil-ladhi 'afani fi jasadi",
                    Meaning = "الحمد لله الذي عافاني في جسدي ورد عليَّ روحي وأذن لي بذكره",
                    Source = "سنن الترمذي — حسن",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
               
                
                
                new Zikr
                {
                    TextArabic = "الْحَمْدُ لِلَّهِ الَّذِي خَلَقَ السَّمَاوَاتِ وَالأَرْضَ وَجَعَلَ الظُّلُمَاتِ وَالنُّورَ",
                    TextTransliteration = "Alhamdulillahil-ladhi khalaqas-samawati wal-ard",
                    Meaning = "الحمد لله الذي خلق السماوات والأرض وجعل الظلمات والنور",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
                new Zikr
                {
                    TextArabic = "أَصْبَحْنَا وَأَصْبَحَ الْمُلْكُ لِلَّهِ رَبِّ الْعَالَمِينَ، اللَّهُمَّ إِنِّي أَسْأَلُكَ خَيْرَ هَذَا الْيَوْمِ: فَتْحَهُ وَنَصْرَهُ وَنُورَهُ وَبَرَكَتَهُ وَهُدَاهُ",
                    TextTransliteration = "Asbahna wa asbahal-mulku lillahi Rabbil-'alamin",
                    Meaning = "أصبحنا وأصبح الملك لله رب العالمين",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
               
                new Zikr
                {
                    TextArabic = "سُبْحَانَكَ اللَّهُمَّ وَبِحَمْدِكَ لَا إِلَهَ إِلَّا أَنْتَ أَسْتَغْفِرُكَ وَأَتُوبُ إِلَيْكَ",
                    TextTransliteration = "Subhanakal-Lahumma wa bihamdika la ilaha illa anta astaghfiruka wa atubu ilayk",
                    Meaning = "سبحانك اللهم وبحمدك أستغفرك وأتوب إليك",
                    Source = "سنن النسائي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.WakeUp
                },
                new Zikr
                {
                    TextArabic = "رَبِّ أَعُوذُ بِكَ مِنْ هَمَزَاتِ الشَّيَاطِينِ وَأَعُوذُ بِكَ رَبِّ أَنْ يَحْضُرُونِ",
                    TextTransliteration = "Rabbi a'udhu bika min hamazatish-shayatin",
                    Meaning = "ربِّ أعوذ بك من همزات الشياطين",
                    Source = "القرآن الكريم — المؤمنون:97-98",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
                new Zikr
                {
                    TextArabic = "أَعُوذُ بِاللهِ السَّمِيعِ الْعَلِيمِ مِنَ الشَّيْطَانِ الرَّجِيمِ",
                    TextTransliteration = "A'udhu billahis-sami'il-'alim minash-shaytanir-rajim",
                    Meaning = "أعوذ بالله السميع العليم من الشيطان الرجيم",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
                
                new Zikr
                {
                    TextArabic = "رَبَّنَا آتِنَا فِي الدُّنْيَا حَسَنَةً وَفِي الآخِرَةِ حَسَنَةً وَقِنَا عَذَابَ النَّارِ",
                    TextTransliteration = "Rabbana atina fid-dunya hasanah",
                    Meaning = "ربنا آتنا في الدنيا حسنة وفي الآخرة حسنة وقنا عذاب النار",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.WakeUp
                },
               
               
            });

            // ================================================================
            // ========== أذكار الصلاة (Prayer) — 40 ذكر ==========
            // ================================================================
            azkar.AddRange(new[]
            {
                // --- أذكار ما بعد الصلاة ---
                new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْمَأْثَمِ وَالْمَغْرَمِ",
    TextTransliteration = "Allahumma inni a'udhu bika minal-ma'thami wal-maghram",
    Meaning = "يقال في التشهد — الاستعاذة من المأثم والمغرم",
    Source = "صحيح البخاري",
    RepeatCount = 1,
    Category = ZikrCategory.Prayer
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنْ عَذَابِ النَّارِ وَمِنْ عَذَابِ الْقَبْرِ وَمِنْ فِتْنَةِ الْمَحْيَا وَالْمَمَاتِ وَمِنْ شَرِّ فِتْنَةِ الْمَسِيحِ الدَّجَّالِ",
    TextTransliteration = "Allahumma inni a'udhu bika min 'adhabin-nar",
    Meaning = "يقال في التشهد الأخير — الاستعاذة الأربع",
    Source = "صحيح مسلم",
    RepeatCount = 1,
    Category = ZikrCategory.Prayer
},
new Zikr
{
    TextArabic = "اللَّهُمَّ اغْفِرْ لِي مَا قَدَّمْتُ وَمَا أَخَّرْتُ وَمَا أَسْرَرْتُ وَمَا أَعْلَنْتُ أَنْتَ إِلَهِي لَا إِلَهَ إِلَّا أَنْتَ",
    TextTransliteration = "Allahummaghfir li ma qaddamtu — variant",
    Meaning = "دعاء في السجود — اللهم اغفر لي ما قدمت وما أخرت",
    Source = "صحيح مسلم",
    RepeatCount = 1,
    Category = ZikrCategory.Prayer
},

                new Zikr
                {
                    TextArabic = "أَسْتَغْفِرُ اللهَ",
                    TextTransliteration = "Astaghfirullah",
                    Meaning = "يقال ثلاثًا بعد الصلاة",
                    Source = "صحيح مسلم",
                    RepeatCount = 3,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ أَنْتَ السَّلَامُ وَمِنْكَ السَّلَامُ تَبَارَكْتَ يَا ذَا الْجَلَالِ وَالإِكْرَامِ",
                    TextTransliteration = "Allahumma antas-Salam wa minkas-Salam tabarakta ya Dhal-Jalali wal-Ikram",
                    Meaning = "اللهم أنت السلام ومنك السلام تباركت يا ذا الجلال والإكرام",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "لَا إِلَهَ إِلَّا اللهُ وَحْدَهُ لَا شَرِيكَ لَهُ لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ، اللَّهُمَّ لَا مَانِعَ لِمَا أَعْطَيْتَ وَلَا مُعْطِيَ لِمَا مَنَعْتَ وَلَا يَنْفَعُ ذَا الْجَدِّ مِنْكَ الْجَدُّ",
                    TextTransliteration = "La ilaha illa Allah wahdahu — post prayer",
                    Meaning = "لا إله إلا الله وحده لا شريك له — بعد الصلاة",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ",
                    TextTransliteration = "Subhan Allah — post prayer",
                    Meaning = "تسبيح بعد الصلاة ثلاثًا وثلاثين",
                    Source = "صحيح مسلم",
                    RepeatCount = 33,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "الْحَمْدُ لِلَّهِ",
                    TextTransliteration = "Alhamdulillah — post prayer",
                    Meaning = "تحميد بعد الصلاة ثلاثًا وثلاثين",
                    Source = "صحيح مسلم",
                    RepeatCount = 33,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللهُ أَكْبَرُ",
                    TextTransliteration = "Allahu Akbar — post prayer",
                    Meaning = "تكبير بعد الصلاة ثلاثًا وثلاثين",
                    Source = "صحيح مسلم",
                    RepeatCount = 33,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "لَا إِلَهَ إِلَّا اللهُ وَحْدَهُ لَا شَرِيكَ لَهُ لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ",
                    TextTransliteration = "La ilaha illa Allah — closing the 99",
                    Meaning = "تُختم بها التسبيحات بعد الصلاة لتكتمل مئة",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                // --- أذكار داخل الصلاة ---
                new Zikr
                {
                    TextArabic = "سُبْحَانَ رَبِّيَ الْعَظِيمِ",
                    TextTransliteration = "Subhana Rabbiyal Azim",
                    Meaning = "تقال في الركوع ثلاثًا",
                    Source = "صحيح مسلم",
                    RepeatCount = 3,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ رَبِّيَ الأَعْلَى",
                    TextTransliteration = "Subhana Rabbiyal A'la",
                    Meaning = "تقال في السجود ثلاثًا",
                    Source = "صحيح مسلم",
                    RepeatCount = 3,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "رَبِّ اغْفِرْ لِي",
                    TextTransliteration = "Rabbighfir li",
                    Meaning = "تقال بين السجدتين ثلاثًا",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "سَمِعَ اللهُ لِمَنْ حَمِدَهُ",
                    TextTransliteration = "Sami' Allahu liman hamidah",
                    Meaning = "يقولها الإمام والمنفرد عند الرفع من الركوع",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "رَبَّنَا وَلَكَ الْحَمْدُ",
                    TextTransliteration = "Rabbana wa lakal-hamd",
                    Meaning = "يقولها المأموم وكذا الإمام بعد قوله سمع الله لمن حمده",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "رَبَّنَا لَكَ الْحَمْدُ حَمْدًا كَثِيرًا طَيِّبًا مُبَارَكًا فِيهِ",
                    TextTransliteration = "Rabbana lakal-hamdu hamdan kathiran tayyiban mubarakan fih",
                    Meaning = "ربنا لك الحمد حمدًا كثيرًا طيبًا مباركًا فيه",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَكَ اللَّهُمَّ وَبِحَمْدِكَ وَتَبَارَكَ اسْمُكَ وَتَعَالَى جَدُّكَ وَلَا إِلَهَ غَيْرُكَ",
                    TextTransliteration = "Subhanakal-Lahumma wa bihamdik wa tabarakasmuk",
                    Meaning = "دعاء الاستفتاح — يقال في أول الصلاة",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "وَجَّهْتُ وَجْهِيَ لِلَّذِي فَطَرَ السَّمَاوَاتِ وَالأَرْضَ حَنِيفًا وَمَا أَنَا مِنَ الْمُشْرِكِينَ",
                    TextTransliteration = "Wajjahtu wajhiya lilladhi fatara as-samawat wal-ard",
                    Meaning = "دعاء الاستفتاح — وجهت وجهي للذي فطر السماوات والأرض",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ بَاعِدْ بَيْنِي وَبَيْنَ خَطَايَايَ كَمَا بَاعَدْتَ بَيْنَ الْمَشْرِقِ وَالْمَغْرِبِ، اللَّهُمَّ نَقِّنِي مِنَ الْخَطَايَا كَمَا يُنَقَّى الثَّوْبُ الأَبْيَضُ مِنَ الدَّنَسِ، اللَّهُمَّ اغْسِلْ خَطَايَايَ بِالْمَاءِ وَالثَّلْجِ وَالْبَرَدِ",
                    TextTransliteration = "Allahumma ba'id bayni wa bayna khatayaya",
                    Meaning = "دعاء الاستفتاح — اللهم باعد بيني وبين خطاياي",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ اغْفِرْ لِي وَارْحَمْنِي وَاهْدِنِي وَعَافِنِي وَارْزُقْنِي",
                    TextTransliteration = "Allahummaghfir li warhamni wahhdini wa 'afini warzuqni",
                    Meaning = "يقال بين السجدتين",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ اغْفِرْ لِي مَا قَدَّمْتُ وَمَا أَخَّرْتُ وَمَا أَسْرَرْتُ وَمَا أَعْلَنْتُ وَمَا أَسْرَفْتُ وَمَا أَنْتَ أَعْلَمُ بِهِ مِنِّي أَنْتَ الْمُقَدِّمُ وَأَنْتَ الْمُؤَخِّرُ لَا إِلَهَ إِلَّا أَنْتَ",
                    TextTransliteration = "Allahummaghfir li ma qaddamtu wa ma akhkhartu",
                    Meaning = "دعاء في آخر الصلاة — اللهم اغفر لي ما قدمت وما أخرت",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنْ عَذَابِ الْقَبْرِ وَمِنْ عَذَابِ جَهَنَّمَ وَمِنْ فِتْنَةِ الْمَحْيَا وَالْمَمَاتِ وَمِنْ شَرِّ فِتْنَةِ الْمَسِيحِ الدَّجَّالِ",
                    TextTransliteration = "Allahumma inni a'udhu bika min 'adhabil-qabr",
                    Meaning = "يقال في التشهد قبل السلام",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي ظَلَمْتُ نَفْسِي ظُلْمًا كَثِيرًا وَلَا يَغْفِرُ الذُّنُوبَ إِلَّا أَنْتَ فَاغْفِرْ لِي مَغْفِرَةً مِنْ عِنْدِكَ وَارْحَمْنِي إِنَّكَ أَنْتَ الْغَفُورُ الرَّحِيمُ",
                    TextTransliteration = "Allahumma inni zalamtu nafsi zulman kathiran",
                    Meaning = "دعاء أبي بكر في الصلاة — يقال في التشهد",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ أَعِنِّي عَلَى ذِكْرِكَ وَشُكْرِكَ وَحُسْنِ عِبَادَتِكَ",
                    TextTransliteration = "Allahumma a'inni 'ala dhikrika wa shukrika wa husni 'ibadatik",
                    Meaning = "يقال في آخر الصلاة قبل السلام — أوصى به النبي ﷺ معاذًا",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْجَنَّةَ وَأَعُوذُ بِكَ مِنَ النَّارِ",
                    TextTransliteration = "Allahumma inni as'alukal-jannah — in prayer",
                    Meaning = "يقال في التشهد الأخير",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "آيَةُ الْكُرْسِيِّ بَعْدَ كُلِّ صَلَاةٍ مَكْتُوبَةٍ",
                    TextTransliteration = "Ayat Al-Kursi after obligatory prayer",
                    Meaning = "من قرأ آية الكرسي دبر كل صلاة مكتوبة لم يمنعه من دخول الجنة إلا أن يموت",
                    Source = "النسائي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                
                new Zikr
                {
                    TextArabic = "لَا إِلَهَ إِلَّا اللهُ وَحْدَهُ لَا شَرِيكَ لَهُ لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ يُحْيِي وَيُمِيتُ وَهُوَ حَيٌّ لَا يَمُوتُ بِيَدِهِ الْخَيْرُ وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ",
                    TextTransliteration = "La ilaha illa Allah wahdahu — after Maghrib and Fajr",
                    Meaning = "يقال عشر مرات بعد المغرب والفجر",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 10,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ عِلْمًا نَافِعًا وَرِزْقًا طَيِّبًا وَعَمَلًا مُتَقَبَّلًا",
                    TextTransliteration = "Allahumma inni as'aluka 'ilman nafi'an — after Fajr",
                    Meaning = "يقال بعد صلاة الفجر",
                    Source = "سنن ابن ماجه — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ وَالْحَمْدُ لِلَّهِ وَلَا إِلَهَ إِلَّا اللهُ وَاللهُ أَكْبَرُ وَلَا حَوْلَ وَلَا قُوَّةَ إِلَّا بِاللهِ",
                    TextTransliteration = "Subhanallah, walhamdulillah, wa la ilaha illallah, wallahu akbar",
                    Meaning = "الباقيات الصالحات",
                    Source = "صحيح مسلم",
                    RepeatCount = 10,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ صَلِّ عَلَى مُحَمَّدٍ وَعَلَى آلِ مُحَمَّدٍ كَمَا صَلَّيْتَ عَلَى إِبْرَاهِيمَ وَعَلَى آلِ إِبْرَاهِيمَ إِنَّكَ حَمِيدٌ مَجِيدٌ",
                    TextTransliteration = "Allahumma salli 'ala Muhammad wa 'ala ali Muhammad",
                    Meaning = "الصلاة الإبراهيمية — تقال في التشهد",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "التَّحِيَّاتُ لِلَّهِ وَالصَّلَوَاتُ وَالطَّيِّبَاتُ السَّلَامُ عَلَيْكَ أَيُّهَا النَّبِيُّ وَرَحْمَةُ اللهِ وَبَرَكَاتُهُ السَّلَامُ عَلَيْنَا وَعَلَى عِبَادِ اللهِ الصَّالِحِينَ أَشْهَدُ أَنْ لَا إِلَهَ إِلَّا اللهُ وَأَشْهَدُ أَنَّ مُحَمَّدًا عَبْدُهُ وَرَسُولُهُ",
                    TextTransliteration = "At-Tahiyyat lillahi was-salawatu watt-tayyibat",
                    Meaning = "التشهد",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْبُخْلِ وَأَعُوذُ بِكَ مِنَ الْجُبْنِ وَأَعُوذُ بِكَ أَنْ أُرَدَّ إِلَى أَرْذَلِ الْعُمُرِ وَأَعُوذُ بِكَ مِنْ فِتْنَةِ الدُّنْيَا وَأَعُوذُ بِكَ مِنْ عَذَابِ الْقَبْرِ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-bukhl",
                    Meaning = "دعاء قبل السلام من الصلاة",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "رَبِّ اجْعَلْنِي مُقِيمَ الصَّلَاةِ وَمِنْ ذُرِّيَّتِي رَبَّنَا وَتَقَبَّلْ دُعَاءِ",
                    TextTransliteration = "Rabbi-j'alni muqimas-salah",
                    Meaning = "دعاء إبراهيم عليه السلام — ربِّ اجعلني مقيم الصلاة",
                    Source = "القرآن الكريم — إبراهيم:40",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "رَبِّ اغْفِرْ لِي وَلِوَالِدَيَّ",
                    TextTransliteration = "Rabbighfir li wa liwalidayya",
                    Meaning = "ربِّ اغفر لي ولوالديَّ — من دعاء القرآن",
                    Source = "القرآن الكريم — نوح:28",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "رَبَّنَا لَا تُؤَاخِذْنَا إِنْ نَسِينَا أَوْ أَخْطَأْنَا",
                    TextTransliteration = "Rabbana la tu'akhidhna in nasina aw akhta'na",
                    Meaning = "ربنا لا تؤاخذنا إن نسينا أو أخطأنا",
                    Source = "القرآن الكريم — البقرة:286",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "رَبَّنَا اغْفِرْ لَنَا وَلِإِخْوَانِنَا الَّذِينَ سَبَقُونَا بِالإِيمَانِ",
                    TextTransliteration = "Rabbana-ghfir lana wa li-ikhwaninal-ladhina sabaquna bil-iman",
                    Meaning = "ربنا اغفر لنا ولإخواننا الذين سبقونا بالإيمان",
                    Source = "القرآن الكريم — الحشر:10",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ رَبَّنَا آتِنَا فِي الدُّنْيَا حَسَنَةً وَفِي الآخِرَةِ حَسَنَةً وَقِنَا عَذَابَ النَّارِ",
                    TextTransliteration = "Rabbana atina fid-dunya hasanah — post prayer",
                    Meaning = "ربنا آتنا في الدنيا حسنة — يدعو بها بعد الصلاة",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنَّ النَّاسَ قَدْ كَذَّبُونِي وَحَبَسُونِي وَشَتَمُونِي — رَبَّنَا اكْشِفْ عَنَّا الْعَذَابَ إِنَّا مُؤْمِنُونَ",
                    TextTransliteration = "Rabbana ikshif 'annal-'adhab inna mu'minun",
                    Meaning = "ربنا اكشف عنا العذاب إنا مؤمنون",
                    Source = "القرآن الكريم — الدخان:12",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "رَبِّ أَعُوذُ بِكَ مِنْ هَمَزَاتِ الشَّيَاطِينِ وَأَعُوذُ بِكَ رَبِّ أَنْ يَحْضُرُونِ",
                    TextTransliteration = "Rabbi a'udhu bika min hamazatish-shayatin — in prayer",
                    Meaning = "أعوذ بك رب أن يحضرون — في الصلاة",
                    Source = "القرآن الكريم — المؤمنون:97-98",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ حَاسِبْنِي حِسَابًا يَسِيرًا",
                    TextTransliteration = "Allahumma hasibnee hisaban yasira",
                    Meaning = "اللهم حاسبني حسابًا يسيرًا",
                    Source = "مسند أحمد — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.Prayer
                },
            });

            // ================================================================
            // ========== أذكار عامة (General) — 60 ذكر ==========
            // ================================================================
            azkar.AddRange(new[]
            {
                // --- التسبيح والتحميد ---
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ وَبِحَمْدِهِ، سُبْحَانَ اللهِ الْعَظِيمِ",
                    TextTransliteration = "Subhan Allahi wa bihamdihi, Subhan Allahil-Azim",
                    Meaning = "كلمتان خفيفتان على اللسان ثقيلتان في الميزان حبيبتان إلى الرحمن",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "لَا إِلَهَ إِلَّا اللهُ",
                    TextTransliteration = "La ilaha illa Allah",
                    Meaning = "أفضل الذكر — لا إله إلا الله",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 100,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللهُ أَكْبَرُ كَبِيرًا وَالْحَمْدُ لِلَّهِ كَثِيرًا وَسُبْحَانَ اللهِ بُكْرَةً وَأَصِيلًا",
                    TextTransliteration = "Allahu akbaru kabira wal-hamdu lillahi kathira wa subhanallahi bukratan wa asila",
                    Meaning = "كلمات وجب لهن في السماء رُكنٌ",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "لَا حَوْلَ وَلَا قُوَّةَ إِلَّا بِاللهِ",
                    TextTransliteration = "La hawla wa la quwwata illa billah",
                    Meaning = "كنز من كنوز الجنة",
                    Source = "صحيح البخاري",
                    RepeatCount = 100,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَ اللهِ وَالْحَمْدُ لِلَّهِ وَلَا إِلَهَ إِلَّا اللهُ وَاللهُ أَكْبَرُ",
                    TextTransliteration = "Subhanallah walhamdulillah wa la ilaha illallah wallahu akbar",
                    Meaning = "أحب الكلام إلى الله",
                    Source = "صحيح مسلم",
                    RepeatCount = 100,
                    Category = ZikrCategory.General
                },
                // --- الاستغفار ---
                new Zikr
                {
                    TextArabic = "أَسْتَغْفِرُ اللهَ الْعَظِيمَ الَّذِي لَا إِلَهَ إِلَّا هُوَ الْحَيُّ الْقَيُّومُ وَأَتُوبُ إِلَيْهِ",
                    TextTransliteration = "Astaghfirullaha al-Azima alladhi la ilaha illa huwal-Hayyul-Qayyum wa atubu ilayh",
                    Meaning = "من قالها غُفرت له ذنوبه وإن كان قد فرَّ من الزحف",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "رَبِّ اغْفِرْ لِي وَتُبْ عَلَيَّ إِنَّكَ أَنْتَ التَّوَّابُ الرَّحِيمُ",
                    TextTransliteration = "Rabbighfir li wa tub 'alayya innaka antat-Tawwabur-Rahim",
                    Meaning = "كان النبي ﷺ يقولها مئة مرة في المجلس الواحد",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 100,
                    Category = ZikrCategory.General
                },
                // --- الصلاة على النبي ---
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ صَلِّ عَلَى مُحَمَّدٍ",
                    TextTransliteration = "Allahumma salli 'ala Muhammad",
                    Meaning = "من صلى عليَّ صلاةً صلى الله عليه بها عشرًا",
                    Source = "صحيح مسلم",
                    RepeatCount = 100,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ صَلِّ عَلَى مُحَمَّدٍ وَعَلَى آلِهِ وَصَحْبِهِ وَسَلِّمْ",
                    TextTransliteration = "Allahumma salli 'ala Muhammad wa 'ala alihi wa sahbihi wa sallim",
                    Meaning = "الصلاة الكاملة على النبي وآله وصحبه",
                    Source = "صحيح البخاري",
                    RepeatCount = 10,
                    Category = ZikrCategory.General
                },
                // --- أذكار الطعام ---
                new Zikr
                {
                    TextArabic = "بِسْمِ اللهِ",
                    TextTransliteration = "Bismillah",
                    Meaning = "يقال عند البدء في الطعام",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "بِسْمِ اللهِ وَعَلَى بَرَكَةِ اللهِ",
                    TextTransliteration = "Bismillahi wa 'ala barakatillah",
                    Meaning = "يقال عند البدء في الطعام",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "الْحَمْدُ لِلَّهِ الَّذِي أَطْعَمَنِي هَذَا وَرَزَقَنِيهِ مِنْ غَيْرِ حَوْلٍ مِنِّي وَلَا قُوَّةٍ",
                    TextTransliteration = "Alhamdulillahil-ladhi at'amani hadha wa razaqanihi min ghayri hawlin minni wa la quwwah",
                    Meaning = "يقال بعد الطعام — غُفر له ما تقدم من ذنبه",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ بَارِكْ لَنَا فِيهِ وَأَطْعِمْنَا خَيْرًا مِنْهُ",
                    TextTransliteration = "Allahumma barik lana fihi wa at'imna khayran minh",
                    Meaning = "يقال عند شرب اللبن",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                // --- أذكار دخول البيت والخروج ---
                new Zikr
                {
                    TextArabic = "بِسْمِ اللهِ وَلَجْنَا وَبِسْمِ اللهِ خَرَجْنَا وَعَلَى اللهِ رَبِّنَا تَوَكَّلْنَا",
                    TextTransliteration = "Bismillahi walajna wa bismillahi kharajna wa 'alallahi rabbina tawakkalna",
                    Meaning = "يقال عند الدخول إلى البيت",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "بِسْمِ اللهِ تَوَكَّلْتُ عَلَى اللهِ وَلَا حَوْلَ وَلَا قُوَّةَ إِلَّا بِاللهِ",
                    TextTransliteration = "Bismillahi tawakkaltu 'alallahi wa la hawla wa la quwwata illa billah",
                    Meaning = "يقال عند الخروج من البيت — يكفى ويوقى ويُهدى",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                // --- أذكار المسجد ---
                new Zikr
                {
                    TextArabic = "أَعُوذُ بِاللهِ الْعَظِيمِ وَبِوَجْهِهِ الْكَرِيمِ وَسُلْطَانِهِ الْقَدِيمِ مِنَ الشَّيْطَانِ الرَّجِيمِ",
                    TextTransliteration = "A'udhu billahil-Azim wa biwajhihil-Karim wa sultanihil-qadim minash-shaytanir-rajim",
                    Meaning = "يقال عند دخول المسجد — قال الشيطان: حُفظ مني سائر اليوم",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ افْتَحْ لِي أَبْوَابَ رَحْمَتِكَ",
                    TextTransliteration = "Allahummaftah li abwaba rahmatik",
                    Meaning = "يقال عند دخول المسجد",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ مِنْ فَضْلِكَ",
                    TextTransliteration = "Allahumma inni as'aluka min fadlik",
                    Meaning = "يقال عند الخروج من المسجد",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                // --- أذكار الوضوء ---
                new Zikr
                {
                    TextArabic = "بِسْمِ اللهِ",
                    TextTransliteration = "Bismillah — before wudu",
                    Meaning = "يقال قبل الوضوء",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "أَشْهَدُ أَنْ لَا إِلَهَ إِلَّا اللهُ وَحْدَهُ لَا شَرِيكَ لَهُ وَأَشْهَدُ أَنَّ مُحَمَّدًا عَبْدُهُ وَرَسُولُهُ",
                    TextTransliteration = "Ashhadu an la ilaha illa Allah wahdahu la sharika lah — after wudu",
                    Meaning = "يقال بعد الوضوء — فُتحت له أبواب الجنة الثمانية",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "سُبْحَانَكَ اللَّهُمَّ وَبِحَمْدِكَ أَشْهَدُ أَنْ لَا إِلَهَ إِلَّا أَنْتَ أَسْتَغْفِرُكَ وَأَتُوبُ إِلَيْكَ",
                    TextTransliteration = "Subhanakal-Lahumma wa bihamdika ashhadu an la ilaha illa ant — after wudu",
                    Meaning = "تقال بعد الوضوء — خُتمت بخاتم ووُضعت تحت العرش",
                    Source = "النسائي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                // --- أذكار السفر والمواصلات ---
                new Zikr
                {
                    TextArabic = "سُبْحَانَ الَّذِي سَخَّرَ لَنَا هَذَا وَمَا كُنَّا لَهُ مُقْرِنِينَ وَإِنَّا إِلَى رَبِّنَا لَمُنْقَلِبُونَ",
                    TextTransliteration = "Subhanal-ladhi sakhkhara lana hadha wa ma kunna lahu muqrinin",
                    Meaning = "يقال عند ركوب الدابة أو المركبة",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنَّا نَسْأَلُكَ فِي سَفَرِنَا هَذَا الْبِرَّ وَالتَّقْوَى وَمِنَ الْعَمَلِ مَا تَرْضَى، اللَّهُمَّ هَوِّنْ عَلَيْنَا سَفَرَنَا هَذَا وَاطْوِ عَنَّا بُعْدَهُ",
                    TextTransliteration = "Allahumma inna nas'aluka fi safarina hadhal-birra wattaqwa",
                    Meaning = "دعاء السفر",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                // --- أذكار المرض والدعاء ---
                new Zikr
                {
                    TextArabic = "بِسْمِ اللهِ أَرْقِيكَ مِنْ كُلِّ شَيْءٍ يُؤْذِيكَ مِنْ شَرِّ كُلِّ نَفْسٍ أَوْ عَيْنٍ حَاسِدٍ اللهُ يَشْفِيكَ بِسْمِ اللهِ أَرْقِيكَ",
                    TextTransliteration = "Bismillahi arqik min kulli shay'in yu'dhik",
                    Meaning = "رقية شرعية — يقولها عند عيادة المريض",
                    Source = "صحيح مسلم",
                    RepeatCount = 3,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ رَبَّ النَّاسِ أَذْهِبِ الْبَأْسَ اشْفِ أَنْتَ الشَّافِي لَا شِفَاءَ إِلَّا شِفَاؤُكَ شِفَاءً لَا يُغَادِرُ سَقَمًا",
                    TextTransliteration = "Allahumma Rabban-nas adhhibil-ba's ishfi antas-Shafi",
                    Meaning = "رقية المريض — اللهم رب الناس أذهب البأس",
                    Source = "صحيح البخاري",
                    RepeatCount = 3,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "لَا بَأْسَ طَهُورٌ إِنْ شَاءَ اللهُ",
                    TextTransliteration = "La ba'sa tahurun in sha'a Allah",
                    Meaning = "يقال للمريض — لا بأس طهور إن شاء الله",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                // --- أذكار اللقاء والوداع ---
                new Zikr
                {
                    TextArabic = "السَّلَامُ عَلَيْكُمْ وَرَحْمَةُ اللهِ وَبَرَكَاتُهُ",
                    TextTransliteration = "As-salamu 'alaykum wa rahmatullahi wa barakatuh",
                    Meaning = "تحية الإسلام",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "وَعَلَيْكُمُ السَّلَامُ وَرَحْمَةُ اللهِ وَبَرَكَاتُهُ",
                    TextTransliteration = "Wa 'alaykumus-salamu wa rahmatullahi wa barakatuh",
                    Meaning = "رد تحية الإسلام",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                // --- أدعية قرآنية ---
                new Zikr
                {
                    TextArabic = "رَبَّنَا تَقَبَّلْ مِنَّا إِنَّكَ أَنْتَ السَّمِيعُ الْعَلِيمُ",
                    TextTransliteration = "Rabbana taqabbal minna innaka antas-Sami'ul-'Alim",
                    Meaning = "ربنا تقبل منا إنك أنت السميع العليم — دعاء إبراهيم وإسماعيل",
                    Source = "القرآن الكريم — البقرة:127",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "رَبَّنَا وَلَا تُحَمِّلْنَا مَا لَا طَاقَةَ لَنَا بِهِ وَاعْفُ عَنَّا وَاغْفِرْ لَنَا وَارْحَمْنَا أَنْتَ مَوْلَانَا فَانْصُرْنَا عَلَى الْقَوْمِ الْكَافِرِينَ",
                    TextTransliteration = "Rabbana wa la tuhammilna ma la taqata lana bihi",
                    Meaning = "ربنا ولا تحملنا ما لا طاقة لنا به",
                    Source = "القرآن الكريم — البقرة:286",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "رَبَّنَا لَا تُزِغْ قُلُوبَنَا بَعْدَ إِذْ هَدَيْتَنَا وَهَبْ لَنَا مِنْ لَدُنْكَ رَحْمَةً إِنَّكَ أَنْتَ الْوَهَّابُ",
                    TextTransliteration = "Rabbana la tuzigh qulubana ba'da idh hadaytana",
                    Meaning = "ربنا لا تزغ قلوبنا بعد إذ هديتنا",
                    Source = "القرآن الكريم — آل عمران:8",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "رَبَّنَا إِنَّنَا آمَنَّا فَاغْفِرْ لَنَا ذُنُوبَنَا وَقِنَا عَذَابَ النَّارِ",
                    TextTransliteration = "Rabbana innana amanna faghfir lana dhunubana wa qina 'adhaban-nar",
                    Meaning = "ربنا إننا آمنا فاغفر لنا ذنوبنا وقنا عذاب النار",
                    Source = "القرآن الكريم — آل عمران:16",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "رَبِّ إِنِّي لِمَا أَنْزَلْتَ إِلَيَّ مِنْ خَيْرٍ فَقِيرٌ",
                    TextTransliteration = "Rabbi inni lima anzalta ilayya min khayrin faqir",
                    Meaning = "ربِّ إني لما أنزلت إليَّ من خير فقير — دعاء موسى عليه السلام",
                    Source = "القرآن الكريم — القصص:24",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "رَبِّ أَوْزِعْنِي أَنْ أَشْكُرَ نِعْمَتَكَ الَّتِي أَنْعَمْتَ عَلَيَّ وَعَلَى وَالِدَيَّ وَأَنْ أَعْمَلَ صَالِحًا تَرْضَاهُ",
                    TextTransliteration = "Rabbi awzi'ni an ashkura ni'matakal-lati an'amta 'alayya wa 'ala walidayya",
                    Meaning = "ربِّ أوزعني أن أشكر نعمتك — دعاء سليمان عليه السلام",
                    Source = "القرآن الكريم — النمل:19",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "لَا إِلَهَ إِلَّا أَنْتَ سُبْحَانَكَ إِنِّي كُنْتُ مِنَ الظَّالِمِينَ",
                    TextTransliteration = "La ilaha illa anta subhanaka inni kuntu minaz-zalimin",
                    Meaning = "دعاء يونس عليه السلام — دعاء الكرب",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 3,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "حَسْبُنَا اللهُ وَنِعْمَ الْوَكِيلُ",
                    TextTransliteration = "Hasbunallahu wa ni'mal-wakil",
                    Meaning = "حسبنا الله ونعم الوكيل — قالها إبراهيم حين أُلقي في النار",
                    Source = "صحيح البخاري",
                    RepeatCount = 3,
                    Category = ZikrCategory.General
                },
                // --- أذكار متنوعة ---
                new Zikr
                  {
    TextArabic = "اللَّهُمَّ رَبَّ هَذِهِ الدَّعْوَةِ التَّامَّةِ وَالصَّلَاةِ الْقَائِمَةِ آتِ مُحَمَّدًا الْوَسِيلَةَ وَالْفَضِيلَةَ وَابْعَثْهُ مَقَامًا مَحْمُودًا الَّذِي وَعَدْتَهُ",
    TextTransliteration = "Allahumma Rabba hadhihid-da'watit-tammah",
    Meaning = "دعاء سماع الأذان — حلَّت له الشفاعة يوم القيامة",
    Source = "صحيح البخاري",
    RepeatCount = 1,
    Category = ZikrCategory.General
                },
              new Zikr  {
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ يَا اللهُ الْأَحَدُ الصَّمَدُ الَّذِي لَمْ يَلِدْ وَلَمْ يُولَدْ وَلَمْ يَكُنْ لَهُ كُفُوًا أَحَدٌ أَنْ تَغْفِرَ لِي ذُنُوبِي",
    TextTransliteration = "Allahumma inni as'aluka ya Allah al-Ahad as-Samad",
    Meaning = "من دعا بالاسم الأعظم أُجيب",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.General },


new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ بِأَنَّ لَكَ الْحَمْدَ لَا إِلَهَ إِلَّا أَنْتَ الْمَنَّانُ بَدِيعُ السَّمَاوَاتِ وَالأَرْضِ يَا ذَا الْجَلَالِ وَالإِكْرَامِ يَا حَيُّ يَا قَيُّومُ",
    TextTransliteration = "Allahumma inni as'aluka bi-anna lakal-hamd",
    Meaning = "الاسم الأعظم — من دعا به أُجيب",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.General
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنْ جَهْدِ الْبَلَاءِ وَدَرَكِ الشَّقَاءِ وَسُوءِ الْقَضَاءِ وَشَمَاتَةِ الأَعْدَاءِ",
    TextTransliteration = "Allahumma inni a'udhu bika min jahdil-bala'",
    Meaning = "الاستعاذة من جهد البلاء ودرك الشقاء",
    Source = "صحيح البخاري",
    RepeatCount = 1,
    Category = ZikrCategory.General
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْعَجْزِ وَالْكَسَلِ وَالْجُبْنِ وَالْهَرَمِ وَالْبُخْلِ وَأَعُوذُ بِكَ مِنْ عَذَابِ الْقَبْرِ وَمِنْ فِتْنَةِ الْمَحْيَا وَالْمَمَاتِ",
    TextTransliteration = "Allahumma inni a'udhu bika minal-'ajzi wal-kasal",
    Meaning = "الاستعاذة الشاملة من آفات النفس",
    Source = "صحيح البخاري",
    RepeatCount = 1,
    Category = ZikrCategory.General
},
new Zikr
{
    TextArabic = "اللَّهُمَّ أَصْلِحْ لِي دِينِيَ الَّذِي هُوَ عِصْمَةُ أَمْرِي وَأَصْلِحْ لِي دُنْيَايَ الَّتِي فِيهَا مَعَاشِي وَأَصْلِحْ لِي آخِرَتِي الَّتِي فِيهَا مَعَادِي",
    TextTransliteration = "Allahumma aslih li dini alladhi huwa 'ismatu amri",
    Meaning = "اللهم أصلح لي ديني ودنياي وآخرتي",
    Source = "صحيح مسلم",
    RepeatCount = 1,
    Category = ZikrCategory.General
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الصِّحَّةَ وَالْعِفَّةَ وَالأَمَانَةَ وَحُسْنَ الْخُلُقِ وَالرِّضَا بِالْقَدَرِ",
    TextTransliteration = "Allahumma inni as'alukas-sihhata wal-'iffah",
    Meaning = "اللهم إني أسألك الصحة والعفة والأمانة وحسن الخلق",
    Source = "مسند أحمد — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.General
},
new Zikr
{
    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الرِّضَا بَعْدَ الْقَضَاءِ وَبَرْدَ الْعَيْشِ بَعْدَ الْمَوْتِ وَلَذَّةَ النَّظَرِ إِلَى وَجْهِكَ وَالشَّوْقَ إِلَى لِقَائِكَ",
    TextTransliteration = "Allahumma inni as'alukar-rida ba'dal-qada'",
    Meaning = "اللهم إني أسألك الرضا بعد القضاء ولذة النظر إلى وجهك",
    Source = "سنن النسائي — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.General
},
new Zikr
{
    TextArabic = "اللَّهُمَّ أَعِنِّي عَلَى ذِكْرِكَ وَشُكْرِكَ وَحُسْنِ عِبَادَتِكَ",
    TextTransliteration = "Allahumma a'inni 'ala dhikrika wa shukrika — General",
    Meaning = "اللهم أعني على ذكرك وشكرك وحسن عبادتك",
    Source = "سنن أبي داود — صحيح",
    RepeatCount = 1,
    Category = ZikrCategory.General
},
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِرِضَاكَ مِنْ سَخَطِكَ وَبِمُعَافَاتِكَ مِنْ عُقُوبَتِكَ وَأَعُوذُ بِكَ مِنْكَ لَا أُحْصِي ثَنَاءً عَلَيْكَ أَنْتَ كَمَا أَثْنَيْتَ عَلَى نَفْسِكَ",
                    TextTransliteration = "Allahumma inni a'udhu biridaka min sakhatik",
                    Meaning = "اللهم إني أعوذ برضاك من سخطك وبمعافاتك من عقوبتك",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ لَا سَهْلَ إِلَّا مَا جَعَلْتَهُ سَهْلًا وَأَنْتَ تَجْعَلُ الْحَزْنَ إِذَا شِئْتَ سَهْلًا",
                    TextTransliteration = "Allahumma la sahla illa ma ja'altahu sahla",
                    Meaning = "اللهم لا سهل إلا ما جعلته سهلًا",
                    Source = "صحيح ابن حبان",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْفَقْرِ وَالْقِلَّةِ وَالذِّلَّةِ وَأَعُوذُ بِكَ مِنْ أَنْ أَظْلِمَ أَوْ أُظْلَمَ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-faqri wal-qillati",
                    Meaning = "اللهم إني أعوذ بك من الفقر والقلة والذلة",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ مِنَ الْخَيْرِ كُلِّهِ عَاجِلِهِ وَآجِلِهِ مَا عَلِمْتُ مِنْهُ وَمَا لَمْ أَعْلَمْ",
                    TextTransliteration = "Allahumma inni as'aluka minal-khayri kullihi 'ajilihi wa ajilihi",
                    Meaning = "اللهم إني أسألك من الخير كله عاجله وآجله",
                    Source = "سنن ابن ماجه — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْهُدَى وَالتُّقَى وَالْعَفَافَ وَالْغِنَى",
                    TextTransliteration = "Allahumma inni as'alukal-huda wat-tuqa wal-'afafa wal-ghina",
                    Meaning = "اللهم إني أسألك الهدى والتقى والعفاف والغنى",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ أَلِّفْ بَيْنَ قُلُوبِنَا وَأَصْلِحْ ذَاتَ بَيْنِنَا وَاهْدِنَا سُبُلَ السَّلَامِ",
                    TextTransliteration = "Allahumma allif bayna qulubina wa aslih dhata baynina",
                    Meaning = "اللهم ألف بين قلوبنا وأصلح ذات بيننا",
                    Source = "مسند أحمد — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ يَا اللهُ بِأَنَّكَ الْوَاحِدُ الأَحَدُ الصَّمَدُ الَّذِي لَمْ يَلِدْ وَلَمْ يُولَدْ وَلَمْ يَكُنْ لَهُ كُفُوًا أَحَدٌ أَنْ تَغْفِرَ لِي ذُنُوبِي إِنَّكَ أَنْتَ الْغَفُورُ الرَّحِيمُ",
                    TextTransliteration = "Allahumma inni as'aluka ya Allah bil-Wahidil-Ahad as-Samad",
                    Meaning = "الاسم الأعظم — من دعا به أُجيب",
                    Source = "سنن أبي داود — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ مُصَرِّفَ الْقُلُوبِ صَرِّفْ قُلُوبَنَا عَلَى طَاعَتِكَ",
                    TextTransliteration = "Allahumma musarrifal-qulub sarrif qulubana 'ala ta'atik",
                    Meaning = "اللهم مصرف القلوب صرف قلوبنا على طاعتك",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ حُبَّكَ وَحُبَّ مَنْ يُحِبُّكَ وَحُبَّ عَمَلٍ يُقَرِّبُنِي إِلَى حُبِّكَ",
                    TextTransliteration = "Allahumma inni as'aluka hubbak wa hubba man yuhibbuk",
                    Meaning = "اللهم إني أسألك حبك وحب من يحبك",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ اجْعَلْنِي مِنَ التَّوَّابِينَ وَاجْعَلْنِي مِنَ الْمُتَطَهِّرِينَ",
                    TextTransliteration = "Allahummaj'alni minat-tawwabin waj'alni minal-mutatahhirin",
                    Meaning = "اللهم اجعلني من التوابين واجعلني من المتطهرين",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ آتِ نَفْسِي تَقْوَاهَا وَزَكِّهَا أَنْتَ خَيْرُ مَنْ زَكَّاهَا أَنْتَ وَلِيُّهَا وَمَوْلَاهَا",
                    TextTransliteration = "Allahumma ati nafsi taqwaha wa zakkiha anta khayru man zakkaha",
                    Meaning = "اللهم آتِ نفسي تقواها وزكها أنت خير من زكاها",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ اهْدِنِي وَسَدِّدْنِي",
                    TextTransliteration = "Allahumma-hdini wa saddidni",
                    Meaning = "اللهم اهدني وسددني — أوصى بها النبي ﷺ عليًّا",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "رَبِّ زِدْنِي عِلْمًا",
                    TextTransliteration = "Rabbi zidni 'ilma",
                    Meaning = "ربِّ زدني علمًا",
                    Source = "القرآن الكريم — طه:114",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "رَبِّ اشْرَحْ لِي صَدْرِي وَيَسِّرْ لِي أَمْرِي وَاحْلُلْ عُقْدَةً مِنْ لِسَانِي يَفْقَهُوا قَوْلِي",
                    TextTransliteration = "Rabbi ishrah li sadri wa yassir li amri wahlul 'uqdatan min lisani yafqahu qawli",
                    Meaning = "ربِّ اشرح لي صدري — دعاء موسى عليه السلام",
                    Source = "القرآن الكريم — طه:25-28",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "إِنَّا لِلَّهِ وَإِنَّا إِلَيْهِ رَاجِعُونَ، اللَّهُمَّ أْجُرْنِي فِي مُصِيبَتِي وَأَخْلِفْ لِي خَيْرًا مِنْهَا",
                    TextTransliteration = "Inna lillahi wa inna ilayhi raji'un, Allahumma ajurni fi musibati",
                    Meaning = "يقال عند المصيبة — فأخلف الله له خيرًا منها",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْعَافِيَةَ فِي الدُّنْيَا وَالآخِرَةِ",
                    TextTransliteration = "Allahumma inni as'alukal-'afiyata fid-dunya wal-akhirah",
                    Meaning = "اللهم إني أسألك العافية في الدنيا والآخرة",
                    Source = "سنن ابن ماجه — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْغَمِّ وَالْهَمِّ وَالْكَسَلِ وَالْهَرَمِ وَالْبُخْلِ وَالْجُبْنِ وَالدَّيْنِ وَغَلَبَةِ الرِّجَالِ",
                    TextTransliteration = "Allahumma inni a'udhu bika minal-ghammi wal-hamm",
                    Meaning = "الاستعاذة الشاملة من المهلكات",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "يَا مُقَلِّبَ الْقُلُوبِ ثَبِّتْ قَلْبِي عَلَى دِينِكَ",
                    TextTransliteration = "Ya Muqallibal-qulub thabbit qalbi 'ala dinik",
                    Meaning = "يا مقلب القلوب ثبت قلبي على دينك",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنَّكَ عَفُوٌّ كَرِيمٌ تُحِبُّ الْعَفْوَ فَاعْفُ عَنِّي",
                    TextTransliteration = "Allahumma innaka 'Afuwwun Karimun tuhibbul-'afwa fa'fu 'anni",
                    Meaning = "دعاء ليلة القدر — اللهم إنك عفو كريم تحب العفو فاعف عني",
                    Source = "سنن الترمذي — صحيح",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَسْأَلُكَ الْفِرْدَوْسَ الأَعْلَى",
                    TextTransliteration = "Allahumma inni as'alukal-Firdawsal-A'la",
                    Meaning = "اللهم إني أسألك الفردوس الأعلى",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ لَكَ أَسْلَمْتُ وَبِكَ آمَنْتُ وَعَلَيْكَ تَوَكَّلْتُ وَإِلَيْكَ أَنَبْتُ وَبِكَ خَاصَمْتُ",
                    TextTransliteration = "Allahumma laka aslamtu wa bika amantu wa 'alayka tawakkaltu",
                    Meaning = "اللهم لك أسلمت وبك آمنت وعليك توكلت",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ اغْفِرْ لِي ذَنْبِي كُلَّهُ دِقَّهُ وَجِلَّهُ وَأَوَّلَهُ وَآخِرَهُ وَعَلَانِيَتَهُ وَسِرَّهُ",
                    TextTransliteration = "Allahummaghfir li dhanbi kullahu diqqahu wa jillahu",
                    Meaning = "اللهم اغفر لي ذنبي كله دقه وجله",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنْ زَوَالِ نِعْمَتِكَ وَتَحَوُّلِ عَافِيَتِكَ وَفُجَاءَةِ نِقْمَتِكَ وَجَمِيعِ سَخَطِكَ",
                    TextTransliteration = "Allahumma inni a'udhu bika min zawali ni'matik",
                    Meaning = "اللهم إني أعوذ بك من زوال نعمتك وتحول عافيتك",
                    Source = "صحيح مسلم",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
                new Zikr
                {
                    TextArabic = "أَعُوذُ بِاللهِ مِنَ الشَّيْطَانِ الرَّجِيمِ",
                    TextTransliteration = "A'udhu billahi minash-shaytanir-rajim",
                    Meaning = "يقال عند الغضب وعند كل وسواس",
                    Source = "صحيح البخاري",
                    RepeatCount = 1,
                    Category = ZikrCategory.General
                },
            });

            await _context.Azkar.AddRangeAsync(azkar);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"✅ تم Seed الأذكار بنجاح — إجمالي {azkar.Count} ذكر");
        }
    }
}
