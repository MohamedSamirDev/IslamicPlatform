using IslamicPlatform.Domain.Entites.Azkar;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Identity
{
    
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
        public ICollection<UserZikrProgress> ZikrProgresses { get; set; }
        public ReadingProgress ReadingProgress { get; set; }
    }
}
