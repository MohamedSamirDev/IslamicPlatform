using Microsoft.AspNetCore.Http;

namespace IslamicPlatform.Api.Controllers.Admin
{
    public class UpdateSheikhRequest
    {
        public string? NameArabic { get; set; }
        public string? NameEnglish { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public IFormFile? Image { get; set; }
    }
}
