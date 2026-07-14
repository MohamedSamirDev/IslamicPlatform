using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.PrayerTimesDTOs
{
    public class NextPrayerDTO
    {
        public string PrayerName { get; set; }
        public string PrayerNameArabic { get; set; }
        public string Time { get; set; }
        public string RemainingTime { get; set; }//الوقت المتبقي
    }
}
