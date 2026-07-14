using IslamicPlatform.Domain.Entites.Quran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Identity
{
    public class ReadingProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LastSurahId { get; set; }
       // public string LastSurahName { get; set; }
        public int LastAyahId { get; set; }
    //    public int LastAyahNumber { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; }
        public Surah LastSurah { get; set; }
        public Ayah LastAyah { get; set; }
    }
}
