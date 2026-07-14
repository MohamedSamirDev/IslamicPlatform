using IslamicPlatform.Domain.Entites.Sheikh;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Data.Configurations.Audio
{
    public class SheikhConfiguration : IEntityTypeConfiguration<Sheikh>
    {
        public void Configure(EntityTypeBuilder<Sheikh> builder)
        {
            builder.ToTable("Sheikhs");
            builder.HasKey(a => a.Id);

            builder.Property(s => s.NameArabic).IsRequired().HasMaxLength(150);
            builder.Property(s => s.NameEnglish).IsRequired().HasMaxLength(150);
            builder.Property(s => s.Country).HasMaxLength(100);
            builder.Property(s => s.ImageUrl).HasMaxLength(500);
            builder.Property(s => s.MoshafType).HasMaxLength(100);
            builder.Property(s => s.ImagePublicId).HasMaxLength(300);

            builder.HasMany(a=>a.Recitations)
                .WithOne(p=>p.Sheikh)
                .HasForeignKey(p=>p.SheikhId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
