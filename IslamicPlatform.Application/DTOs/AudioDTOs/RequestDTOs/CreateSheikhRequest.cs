using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IslamicPlatform.Api.Controllers.Admin
{
    public class CreateSheikhRequest
    {
        [Required]
        public string NameArabic { get; set; }
        [Required]
        public string NameEnglish { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public string? MoshafType { get; set; }
        public IFormFile? Image { get; set; }
    }
}
