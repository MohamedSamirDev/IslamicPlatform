using IslamicPlatform.Domain.Entites.hadith;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Data.Configurations.hadith
{
    public class HadithChapterConfiguration : IEntityTypeConfiguration<HadithChapter>
    {
        public void Configure(EntityTypeBuilder<HadithChapter> builder)
        {
            builder.ToTable("HadithChapters");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameArabic).IsRequired().HasMaxLength(300);
            builder.Property(c => c.NameEnglish).HasMaxLength(300);

            builder.HasMany(c => c.Hadiths)
                   .WithOne(h => h.Chapter)
                   .HasForeignKey(h => h.ChapterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
