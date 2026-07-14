using IslamicPlatform.Api.Models.Requests;
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
    [Authorize]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService) => _bookmarkService = bookmarkService;

        [HttpGet]
        public async Task<IActionResult> GetMyBookmarks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _bookmarkService.GetUserBookmarksAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookmark([FromBody] AddBookmarkRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _bookmarkService.AddBookmarkAsync(userId, request.Type, request.EntityId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{bookmarkId}")]
        public async Task<IActionResult> RemoveBookmark(int bookmarkId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _bookmarkService.RemoveBookmarkAsync(userId, bookmarkId);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("check")]
        public async Task<IActionResult> IsBookmarked([FromQuery] BookmarkType type, [FromQuery] int entityId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _bookmarkService.IsBookmarkedAsync(userId, type, entityId);
            return Ok(result);
        }
    }

    
   
}
