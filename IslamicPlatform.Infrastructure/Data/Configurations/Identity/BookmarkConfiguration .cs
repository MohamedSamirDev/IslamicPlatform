using IslamicPlatform.Domain.Entites.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Data.Configurations.Identity
{
    
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.ToTable("Bookmarks");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Type).HasConversion<string>();

            builder.HasOne(b => b.Ayah)
                   .WithMany(a => a.Bookmarks)
                   .HasForeignKey(b => b.AyahId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Hadith)
                   .WithMany(h => h.Bookmarks)
                   .HasForeignKey(b => b.HadithId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
