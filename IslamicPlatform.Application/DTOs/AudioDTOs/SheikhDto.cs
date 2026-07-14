using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.AudioDTOs
{
    public class SheikhDto
    {
        public int Id { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string Country { get; set; }
        public string? ImageUrl { get; set; }
        public string? MoshafType { get; set; }
        public string? Bio { get; set; }
    }
}
