using IslamicPlatform.Domain.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Quran
{
    public class Ayah
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int NumberInSurah { get; set; }
        public  string TextArabic { get; set; }
        public string? TextArabicNormalized { get; set; }
        public int SurahId { get; set; }

        private int _juzNumber;
        public int JuzNumber
        {
            get => _juzNumber;
            set
            {
                if (value < 1 || value > 30)
                    throw new ArgumentOutOfRangeException(nameof(JuzNumber), "الجزء لازم يكون بين 1 و 30");
                _juzNumber = value;
            }
        }

        private int _hizbNumber;
        public int HizbNumber
        {
            get => _hizbNumber;
            set
            {
                if (value < 1 || value > 60)
                    throw new ArgumentOutOfRangeException(nameof(HizbNumber), "الحزب لازم يكون بين 1 و 60");
                _hizbNumber = value;
            }
        }

        private int _rubNumber;
        public int RubNumber
        {
            get => _rubNumber;
            set
            {
                if (value < 1 || value > 240)
                    throw new ArgumentOutOfRangeException(nameof(RubNumber), "الربع لازم يكون بين 1 و 240");
                _rubNumber = value;
            }
        }

        public Surah Surah { get; set; }
        public ICollection<AyahTranslation> Translations { get; set; } = new List<AyahTranslation>();
        public ICollection<Tafseer> Tafseers { get; set; } = new List<Tafseer>();
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
    }

}

