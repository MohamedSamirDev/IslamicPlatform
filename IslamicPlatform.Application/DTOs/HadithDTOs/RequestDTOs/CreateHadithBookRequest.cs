using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.HadithDTOs.CreateDTOs
{
    public class CreateHadithBookRequest
    {
        [Required] 
        public string NameArabic { get; set; }
        [Required]
        public string NameEnglish { get; set; }
        public string? Author { get; set; }
    }
}
