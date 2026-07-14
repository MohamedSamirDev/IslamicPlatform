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
    public class HadithConfiguration : IEntityTypeConfiguration<Hadith>
    {
        public void Configure(EntityTypeBuilder<Hadith> builder)
        {
            builder.ToTable("Hadiths");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.TextArabic).IsRequired(); 
        
            builder.Property(h => h.TextEnglish).HasColumnType("nvarchar(max)");   
            builder.Property(h => h.Narrator).HasColumnType("nvarchar(max)");

            builder.Property(h => h.Grade).HasMaxLength(500); 
            builder.Property(h => h.Number).IsRequired();
        }
    }
}
