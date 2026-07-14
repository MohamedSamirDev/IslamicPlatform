using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.hadith
{
    public class HadithBook
    {
        public int Id { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string Author { get; set; }

        public ICollection<HadithChapter> Chapters { get; set; }
    }
}
