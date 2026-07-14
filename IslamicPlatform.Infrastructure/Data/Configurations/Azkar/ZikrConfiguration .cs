using IslamicPlatform.Domain.Entites.Azkar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Data.Configurations.Azkar
{
    public class ZikrConfiguration : IEntityTypeConfiguration<Zikr>
    {
        public void Configure(EntityTypeBuilder<Zikr> builder)
        {
            builder.ToTable("Azkar");
            builder.HasKey(z => z.Id);

            builder.Property(z => z.TextArabic).IsRequired();
            builder.Property(z => z.Meaning).HasMaxLength(1000);
            builder.Property(z => z.Source).HasMaxLength(300);
            builder.Property(z => z.Category).HasConversion<string>();
            builder.Property(z => z.RepeatCount).IsRequired();

            builder.HasMany(z => z.UserProgresses)
                   .WithOne(p => p.Zikr)
                   .HasForeignKey(p => p.ZikrId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
