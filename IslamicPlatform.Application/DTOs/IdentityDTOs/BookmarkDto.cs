using IslamicPlatform.Application.DTOs.HadithDTOs;
using IslamicPlatform.Application.DTOs.Quran;
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.IdentityDTOs
{
    public class BookmarkDto
    {
        public int Id { get; set; }      
        public BookmarkType Type { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public AyahDto? Ayah { get; set; }
        public HadithDto? Hadith { get; set; }
    }
}
