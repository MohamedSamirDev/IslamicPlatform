using IslamicPlatform.Domain.Entites.Sheikh;
using IslamicPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Quran
{
    public class Surah
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string NameArabic { get; set; } 
        public string NameEnglish { get; set; } 
        public string NameTransliteration { get; set; } //= Al-Fatiha
        public string? NameArabicNormalized { get; set; }
        public int AyahCount { get; set; }
        public RevelationType RevelationType { get; set;  }
        public int JuzNumber { get; set; }

        public ICollection<Ayah> Ayahs { get; set; } = new List<Ayah>();
        public ICollection<Recitation> Recitations { get; set; } = new List<Recitation>();

    }
}
