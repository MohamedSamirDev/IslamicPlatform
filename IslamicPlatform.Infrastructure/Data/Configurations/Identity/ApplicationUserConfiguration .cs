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
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.Property(u => u.FullName).IsRequired().HasMaxLength(200);
            builder.Property(u => u.ImageUrl).HasMaxLength(500);
            builder.Property(u => u.Country).HasMaxLength(100);


            builder.HasMany(a=>a.RefreshTokens)
                .WithOne(p=>p.User)
                .HasForeignKey(p=>p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Bookmarks)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.ZikrProgresses)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.ReadingProgress)
              .WithOne(r => r.User)
              .HasForeignKey<ReadingProgress>(r => r.UserId)
              .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
