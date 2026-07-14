using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.IdentityDTOs
{
    public class ReadingProgressDto
    {
        public int LastSurahId { get; set; }
        public string LastSurahName { get; set; }
        public int LastAyahId { get; set; }
        public int LastAyahNumber { get; set; }
        public DateTime UpdatedAt { get; set; } 
    }
}
