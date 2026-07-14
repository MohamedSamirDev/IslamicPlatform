using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Quran
{
    public class AyahTranslation
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Language { get; set; } 
        public string TranslatorName { get; set; } 
        public int AyahId { get; set; }

        public Ayah Ayah { get; set; } = null!;
    }
}
