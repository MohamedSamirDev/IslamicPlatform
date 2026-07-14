using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.AI
{
    public class AIResponseDto
    {
        public string Answer {  get; set; }
        public bool IsFromDatabase { get; set; }
        public List<string> RelatedAyahs { get; set; }
        public List<string> RelatedHadiths { get; set; }
        public bool IsKnown { get; set; }
    }
}
