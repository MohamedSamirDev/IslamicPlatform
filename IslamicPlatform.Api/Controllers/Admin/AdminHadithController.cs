using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.HadithDTOs;
using IslamicPlatform.Application.DTOs.HadithDTOs.CreateDTOs;
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IslamicPlatform.Api.Controllers.Admin
{
   
    [ApiController]
    [Route("api/admin/hadith")]
    [Authorize(Roles = "Admin")]
    public class AdminHadithController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminHadithController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpPost("books")]
        public async Task<IActionResult> CreateBook([FromBody] CreateHadithBookRequest request)
        {
            var book = new HadithBook
            {
                NameArabic = request.NameArabic,
                NameEnglish = request.NameEnglish,
                Author = request.Author
            };

            await _unitOfWork.Hadiths.AddBookAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return Ok(ApiResponse<HadithBookDto>.Ok(new HadithBookDto
            {
                Id=book.Id,
                NameArabic=book.NameArabic,
                NameEnglish=book.NameEnglish,
                Author=book.Author
            }, "Book created successfully"));
        }

        [HttpPost("chapters")]
        public async Task<IActionResult> CreateChapter([FromBody] CreateHadithChapterRequest request)
        {
            var book = await _unitOfWork.Hadiths.GetAllBooksAsync();
            if (!book.Any(b => b.Id == request.BookId))
                return NotFound(ApiResponse<string>.Fail("Book Not Found"));

            var chapter = new HadithChapter
            {
                NameArabic = request.NameArabic,
                NameEnglish = request.NameEnglish,
                BookId = request.BookId
            };

            await _unitOfWork.Hadiths.AddChapterAsync(chapter);
            await _unitOfWork.SaveChangesAsync();

            return Ok(ApiResponse<HadithChapterDto>.Ok(new HadithChapterDto
            {
                Id=chapter.Id,
                NameEnglish=chapter.NameEnglish,
                NameArabic=chapter.NameArabic,
                BookId=chapter.BookId,
                BookName = book.First(b=>b.Id==request.BookId).NameArabic,
                
            }, "Chapter created successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateHadith([FromBody] CreateHadithRequest request)
        {
            var chapter = await _unitOfWork.Hadiths.GetChaptersByBookAsync(request.ChapterId);
            if (chapter == null || !chapter.Any())
                return NotFound(ApiResponse<string>.Fail("Chapter Not Found"));
               
            var hadith = new Hadith
            {
                Number = request.Number,
                TextArabic = request.TextArabic,
                TextEnglish = request.TextEnglish,
                Narrator = request.Narrator,
                Grade = request.Grade,
                ChapterId = request.ChapterId
            };

            await _unitOfWork.Hadiths.AddAsync(hadith);
            await _unitOfWork.SaveChangesAsync();

            return Ok(ApiResponse<HadithDto>.Ok(new HadithDto
            {
                Id = hadith.Id,
                Number = hadith.Number,
                TextArabic = hadith.TextArabic,
                TextEnglish = hadith.TextEnglish,
                Narrator = hadith.Narrator,
                Grade = hadith.Grade,
                ChapterName = chapter.First().NameArabic,
                BookName = ""

            }, "Hadith created successfully"));
        }
    }

   

   

   
        
}
