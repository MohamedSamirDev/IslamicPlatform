using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.HadithDTOs.CreateDTOs
{
    public class CreateHadithChapterRequest
    {
        [Required]
        public string NameArabic { get; set; }
        public string? NameEnglish { get; set; }
        [Required] 
        public int BookId { get; set; }
    }
}
