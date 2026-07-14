using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Sheikh
{
    public class Sheikh
    {
        public int Id { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string Country { get; set; }
        public string Bio { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImagePublicId { get; set; }//ImagePublicId: ID فريد للصورة في Cloudinary يُستخدم لإدارتها (حذف/تعديل).
        public string MoshafType { get; set; }/*Uthmani,       المصحف العثماني(الرسمي
                                                 Simple,        نص بسيط بدون تشكيل كامل
                                                 Tajweed,      ملون للتجويد
                                                 Warsh,         رواية ورش عن نافع
                                                  Hafs          رواية حفص عن عاصم*/
        public ICollection<Recitation> Recitations { get; set; }
    }
}
