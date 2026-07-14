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
    public class AyahConfiguration : IEntityTypeConfiguration<Ayah>
    {
        public void Configure(EntityTypeBuilder<Ayah> builder)
        {
            builder.ToTable("Ayahs");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.TextArabic).IsRequired();
            builder.Property(a => a.Number).IsRequired();
            builder.Property(a => a.NumberInSurah).IsRequired();
            builder.Property(a => a.JuzNumber).IsRequired();
            builder.Property(a => a.HizbNumber).IsRequired();
            builder.Property(a => a.RubNumber).IsRequired();
            

            builder.HasMany(p => p.Translations)
                .WithOne(a => a.Ayah)
                .HasForeignKey(a => a.AyahId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p=> p.Tafseers)
                .WithOne(a => a.Ayah)
                .HasForeignKey(a => a.AyahId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Bookmarks)
                .WithOne(a => a.Ayah)
                .HasForeignKey(p => p.AyahId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
