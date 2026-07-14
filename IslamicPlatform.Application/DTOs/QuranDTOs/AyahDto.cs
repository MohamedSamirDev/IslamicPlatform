using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.Quran
{
    
    public class AyahDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int NumberInSurah { get; set; }
        public string TextArabic { get; set; }
        public int JuzNumber { get; set; }
        public int HizbNumber { get; set; } 
        public int RubNumber { get; set; }         
        public List<AyahTranslationDto> Translations { get; set; } = new();
        public List<TafseerDto> Tafseers { get; set; } = new();
    }
}
