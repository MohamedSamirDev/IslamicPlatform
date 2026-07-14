using IslamicPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.AzkarDTOs.RequestDTOs
{
    public class CreateZikrRequest
    {
       [Required]
        public string TextArabic { get; set; }
        public string? TextTransliteration { get; set; }
        public string? Meaning { get; set; }
        public string? Source { get; set; }
        [Required] 
        public int RepeatCount { get; set; }
        [Required]
        public ZikrCategory Category { get; set; }
    }
}
