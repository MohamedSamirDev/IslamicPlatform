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
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Token).IsRequired().HasMaxLength(500);
            builder.Property(r => r.DeviceName).HasMaxLength(200);
            builder.Property(r => r.ReplacedByToken).HasMaxLength(500);

            builder.HasIndex(r => r.Token).IsUnique();
        }
    }
}
