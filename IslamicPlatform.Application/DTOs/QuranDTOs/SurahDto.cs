using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.Quran
{
    public class SurahDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string NameTransliteration { get; set; }
        public int AyahCount { get; set; }
        public string RevelationType { get; set; }
        public int JuzNumber { get; set; }
    }
}
