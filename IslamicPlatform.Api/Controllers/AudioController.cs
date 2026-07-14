using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly IAudioService _audioService;

        public AudioController(IAudioService audioService)=>_audioService= audioService;

        [HttpGet("sheikhs")]
        public async Task<IActionResult> GetAllSheikhs()
        {
            var result = await _audioService.GetAllSheikhsAsync();
            return Ok(result);
        }

        [HttpGet("sheikhs/{sheikhId}")]
        public async Task<IActionResult> GetSheikhRecitations(int sheikhId)
        {
            var result = await _audioService.GetSheikhRecitationsAsync(sheikhId);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("sheikhs/{sheikhId}/surahs/{surahId}")]
        public async Task<IActionResult> GetRecitation(int sheikhId, int surahId)
        {
            var result = await _audioService.GetRecitationAsync(sheikhId, surahId);
            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpPost("sheikhs/{sheikhId}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadSheikhImage(int sheikhId, IFormFile file)
        {
            // تحقق إن الملف موجود
            if (file == null || file.Length == 0)
                return BadRequest(ApiResponse<string>.Fail("لم يتم رفع أي ملف"));

            // تحقق من نوع الملف
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
                return BadRequest(ApiResponse<string>.Fail("يسمح فقط بصور JPEG و PNG و WebP"));

            // تحقق من حجم الملف — max 5MB
            if (file.Length > 5 * 1024 * 1024)
                return BadRequest(ApiResponse<string>.Fail("حجم الصورة يجب أن يكون أقل من 5MB"));

            var result = await _audioService.UploadSheikhImageAsync(sheikhId, file);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
