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
   
    public class ReadingProgressConfiguration : IEntityTypeConfiguration<ReadingProgress>
    {
        public void Configure(EntityTypeBuilder<ReadingProgress> builder)
        {
            builder.ToTable("ReadingProgresses");
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.LastSurah)
                   .WithMany()
                   .HasForeignKey(r => r.LastSurahId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.LastAyah)
                   .WithMany()
                   .HasForeignKey(r => r.LastAyahId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
