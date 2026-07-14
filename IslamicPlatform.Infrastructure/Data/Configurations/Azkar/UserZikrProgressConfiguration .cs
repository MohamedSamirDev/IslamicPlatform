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
    public class UserZikrProgressConfiguration : IEntityTypeConfiguration<UserZikrProgress>
    {
        public void Configure(EntityTypeBuilder<UserZikrProgress> builder)
        {
            builder.ToTable("UserZikrProgresses");
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => new { p.UserId, p.ZikrId }).IsUnique();

        }
    }
}
