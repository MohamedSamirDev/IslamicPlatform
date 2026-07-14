using IslamicPlatform.Application.DTOs.Quran;
using IslamicPlatform.Domain.Entites.Quran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.AudioDTOs
{
  
    public class RecitationDto
    {
        public int Id { get; set; }
        public string AudioUrl { get; set; }
        public string Quality { get; set; }
        public int DurationSeconds { get; set; }
        public SurahDto Surah { get; set; }
        public SheikhDto Sheikh { get; set; }
    }
}
