using IslamicPlatform.Domain.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.hadith
{
    public class Hadith
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string TextArabic { get; set; }
        public string TextEnglish { get; set; }
        public string TextArabicSearch { get; set; } = string.Empty;
        public string Narrator { get; set; }
        public string Grade { get; set; }
        public int ChapterId { get; set; }

        public HadithChapter Chapter { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
