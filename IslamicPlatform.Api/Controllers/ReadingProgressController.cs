using IslamicPlatform.Api.Models.Requests;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IslamicPlatform.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReadingProgressController : ControllerBase
    {
        private readonly IReadingProgressService _readingProgressService;

        public ReadingProgressController(IReadingProgressService readingProgressService)
            => _readingProgressService = readingProgressService;

        [HttpGet]
        public async Task<IActionResult> GetMyProgress()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _readingProgressService.GetProgressAsync(userId);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProgress([FromBody] UpdateProgressRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _readingProgressService.UpdateProgressAsync(userId, request.SurahId, request.AyahId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }

   
    
}
