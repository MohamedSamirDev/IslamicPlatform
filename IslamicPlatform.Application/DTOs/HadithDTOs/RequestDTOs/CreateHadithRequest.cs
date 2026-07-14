using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.HadithDTOs.CreateDTOs
{
    public class CreateHadithRequest
    {
        [Required] 
        public int Number { get; set; }
        [Required]
        public string TextArabic { get; set; }
        public string? TextEnglish { get; set; }
        public string? Narrator { get; set; }
        public string? Grade { get; set; }

        [Required]
        public int ChapterId { get; set; }
    }
}

