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
    public class AyahTranslationConfiguration : IEntityTypeConfiguration<AyahTranslation>
    {
        public void Configure(EntityTypeBuilder<AyahTranslation> builder)
        {
            builder.ToTable("AyahTranslations");
            builder.HasKey(a => a.Id);

            builder.Property(t => t.Text).IsRequired();
            builder.Property(t => t.Language).IsRequired().HasMaxLength(10);
            builder.Property(t => t.TranslatorName).HasMaxLength(150);
        }
    }
}
