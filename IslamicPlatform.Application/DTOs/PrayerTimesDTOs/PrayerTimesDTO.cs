using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.PrayerTimesDTOs
{
    public class PrayerTimesDTO
    {
        public string Fajr { get; set; }
        public string Sunrise { get; set; }
        public string Dhuhr { get; set; }
        public string Asr { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
