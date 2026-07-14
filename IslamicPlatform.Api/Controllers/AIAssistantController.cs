using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AI;
using IslamicPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPlatform.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AIAssistantController : ControllerBase
    {
        private readonly IAIAssistantSevices _aiService;

        public AIAssistantController(IAIAssistantSevices aiService)
            => _aiService = aiService;

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] AskQuestionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail("السؤال مطلوب"));

            var result = await _aiService.AskQuestionAsync(dto.Question);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
