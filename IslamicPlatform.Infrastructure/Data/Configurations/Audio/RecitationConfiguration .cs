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
    public class RecitationConfiguration : IEntityTypeConfiguration<Recitation>
    {
        public void Configure(EntityTypeBuilder<Recitation> builder)
        {
            builder.ToTable("Recitations");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.AudioUrl).IsRequired().HasMaxLength(500);
            builder.Property(r => r.Quality).HasMaxLength(50);

            builder.HasIndex(r => new { r.SheikhId, r.SurahId }).IsUnique();
        }
    }
}
