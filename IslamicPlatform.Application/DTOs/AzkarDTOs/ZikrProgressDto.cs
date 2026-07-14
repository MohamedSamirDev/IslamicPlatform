using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.AzkarDTOs
{
    public class ZikrProgressDto
    {
        public int ZikrId { get; set; }
        public string TextArabic { get; set; }
        public int RepeatCount { get; set; }
        public int CurrentCount { get; set; }
        public bool IsCompleted { get; set; }
    }
}
