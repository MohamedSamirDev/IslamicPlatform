using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.hadith
{
    public class HadithChapter
    {
        //الصلاه - الوضوء - الصيام - الحج - الزكاة - الجهاد - الاخلاق - الاداب - العقيدة - التفسير - الفقه - السيرة النبوية
        public int Id { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public int BookId { get; set; }

        public HadithBook Book { get; set; }
        public ICollection<Hadith> Hadiths { get; set; }
    }
}
