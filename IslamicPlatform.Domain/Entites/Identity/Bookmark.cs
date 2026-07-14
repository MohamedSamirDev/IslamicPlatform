
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Identity
{
    public class Bookmark
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public BookmarkType Type { get; set; }
        public int? AyahId { get; set; }
        public int? HadithId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; }
        public Ayah? Ayah { get; set; }

        public Hadith? Hadith { get; set; }

       
 

    }
}
