using IslamicPlatform.Application.DTOs.Common;
using IslamicPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HadithController : ControllerBase
    {
        private readonly IHadithService _hadithService;

        public HadithController(IHadithService hadithService) => _hadithService = hadithService;

        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _hadithService.GetAllBooksAsync();
            return Ok(result);
        }



        [HttpGet("chapters/{chapterId}")]
        public async Task<IActionResult> GetByChapter(int chapterId)
        {
            var result = await _hadithService.GetByChapterAsync(chapterId);
            return Ok(result);
        }
        [HttpGet("{bookId}/chapters")]
        public async Task<IActionResult> GetChaptersByBook(int bookId)
        {
            var result= await _hadithService.GetChaptersByBookAsync(bookId);
            return Ok(result);
        }


        [HttpGet("books/{bookId}")]
        public async Task<IActionResult> GetByBook(int bookId, [FromQuery] PaginationParams pagination)
        {
            var result = await _hadithService.GetByBookPagedAsync(bookId, pagination);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword, [FromQuery] PaginationParams pagination)
        {
            var result = await _hadithService.SearchPagedAsync(keyword, pagination);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
