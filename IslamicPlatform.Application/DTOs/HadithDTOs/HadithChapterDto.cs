using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.HadithDTOs
{
    public class HadithChapterDto
    {
        public int Id { get; set; }
        public string NameArabic { get; set; }
        public string? NameEnglish { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
    }
}
