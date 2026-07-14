using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.DTOs.AI
{
    public class AskQuestionDto
    {
        [Required]
        [MinLength(5)]
        public string Question { get; set; }
    }
}
