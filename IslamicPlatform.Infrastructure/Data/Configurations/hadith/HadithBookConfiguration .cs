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
    public class HadithBookConfiguration : IEntityTypeConfiguration<HadithBook>
    {
        public void Configure(EntityTypeBuilder<HadithBook> builder)
        {
            builder.ToTable("HadithBooks");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.NameArabic).IsRequired().HasMaxLength(200);
            builder.Property(b => b.NameEnglish).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Author).HasMaxLength(200);

            builder.HasMany(b => b.Chapters)
                   .WithOne(c => c.Book)
                   .HasForeignKey(c => c.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
