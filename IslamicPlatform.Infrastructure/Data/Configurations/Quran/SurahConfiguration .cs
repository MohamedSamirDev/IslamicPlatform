using IslamicPlatform.Domain.Entites.Quran;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Data.Configurations.Quran
{
    public class SurahConfiguration : IEntityTypeConfiguration<Surah>
    {
        public void Configure(EntityTypeBuilder<Surah> builder)
        {
            builder.ToTable("Surahs");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Number).IsRequired();
            builder.Property(s => s.NameArabic).IsRequired().HasMaxLength(50);
            builder.Property(s => s.NameEnglish).IsRequired().HasMaxLength(100);
            builder.Property(s => s.NameTransliteration).HasMaxLength(100);
            builder.Property(s => s.RevelationType).HasConversion<string>();

            builder.HasMany(p => p.Ayahs)
                .WithOne(p => p.Surah)
                .HasForeignKey(p => p.SurahId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Recitations)
                .WithOne(p => p.Surah)
                .HasForeignKey(p => p.SurahId)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
