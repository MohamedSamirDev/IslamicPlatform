using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.AzkarDTOs
{
    public class ZikrDto
    {
        public int Id { get; set; }
        public string TextArabic { get; set; }
        public string TextTransliteration { get; set; }
        public string Meaning { get; set; }
        public string Source { get; set; }
        public int RepeatCount { get; set; }
        public string Category { get; set; }
    }
}
