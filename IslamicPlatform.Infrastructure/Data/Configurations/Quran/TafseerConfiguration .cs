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
    public class TafseerConfiguration : IEntityTypeConfiguration<Tafseer>
    {
        public void Configure(EntityTypeBuilder<Tafseer> builder)
        {
            builder.ToTable("Tafseers");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Text).IsRequired();
            builder.Property(a => a.ScholarName).IsRequired().HasMaxLength(150);
        }
    }
}
