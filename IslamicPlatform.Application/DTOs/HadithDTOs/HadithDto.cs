using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.HadithDTOs
{
    public class HadithDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string TextArabic { get; set; }
        public string? TextEnglish { get; set; }
        public string? Narrator { get; set; }
        public string? Grade { get; set; }
        public string ChapterName { get; set; }
        public int ChapterId { get; set; }
        public string BookName { get; set; }
    }
}
