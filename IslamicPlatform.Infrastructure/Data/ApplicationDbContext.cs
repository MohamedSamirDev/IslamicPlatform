using IslamicPlatform.Domain.Entites.Azkar;
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Entites.Sheikh;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Quran
        public DbSet<Surah> Surahs { get; set; }
        public DbSet<Ayah> Ayahs { get; set; }
        public DbSet<AyahTranslation> AyahTranslations { get; set; }
        public DbSet<Tafseer> Tafseers { get; set; }

        // Audio
        public DbSet<Sheikh> Sheikhs { get; set; }
        public DbSet<Recitation> Recitations { get; set; }

        // Azkar
        public DbSet<Zikr> Azkar { get; set; }
        public DbSet<UserZikrProgress> UserZikrProgresses { get; set; }

        // Hadith
        public DbSet<HadithBook> HadithBooks { get; set; }
        public DbSet<HadithChapter> HadithChapters { get; set; }
        public DbSet<Hadith> Hadiths { get; set; }

        // Identity
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<ReadingProgress> ReadingProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
