using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IslamicPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AzkarController : ControllerBase
    {
        private readonly IAzkarService _azkarService;

        public AzkarController(IAzkarService azkarService) => _azkarService = azkarService;

        [HttpGet("{category}")]
        public async Task<IActionResult> GetByCategory(ZikrCategory category)
        {
            var result = await _azkarService.GetByCategoryAsync(category);
            return Ok(result);
        }

        [HttpGet("{category}/progress")]
        [Authorize]
        public async Task<IActionResult> GetUserProgress(ZikrCategory category)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _azkarService.GetUserProgressAsync(userId, category);
            return Ok(result);
        }

        [HttpPost("{zikrId}/increment")]
        [Authorize]
        public async Task<IActionResult> Increment(int zikrId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _azkarService.IncrementCountAsync(userId, zikrId);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("reset")]
        [Authorize]
        public async Task<IActionResult> ResetDaily()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _azkarService.ResetDailyProgressAsync(userId);
            return Ok(result);
        }
    }
}