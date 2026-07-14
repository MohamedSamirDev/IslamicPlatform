using IslamicPlatform.Domain.Entites.Quran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Sheikh
{
    public class Recitation
    {
        public int Id { get; set; }
        public string AudioUrl { get; set; }
        public string Quality { get; set; }
        public int DurationSeconds { get; set; }
        public int SheikhId { get; set; }
        public int SurahId { get; set; }

        public Sheikh Sheikh { get; set; }
        public Surah Surah { get; set; }
        
    }
}
