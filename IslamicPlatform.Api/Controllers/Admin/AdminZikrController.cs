using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AzkarDTOs;
using IslamicPlatform.Application.DTOs.AzkarDTOs.RequestDTOs;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.Azkar;
using IslamicPlatform.Domain.Enums;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IslamicPlatform.Api.Controllers.Admin
{
    
    [ApiController]
    [Route("api/admin/azkar")]
    [Authorize(Roles = "Admin")]
    public class AdminZikrController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cache;

        public AdminZikrController(IUnitOfWork unitOfWork, ICacheService cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> CreateZikr([FromBody] CreateZikrRequest request)
        {
            var zikr = new Zikr
            {
                TextArabic = request.TextArabic,
                TextTransliteration = request.TextTransliteration,
                Meaning = request.Meaning,
                Source = request.Source,
                RepeatCount = request.RepeatCount,
                Category = request.Category
            };

            await _unitOfWork.Azkar.AddAsync(zikr);
            await _unitOfWork.SaveChangesAsync();

            // امسح الـ Cache عشان الـ Data الجديدة تظهر
            await _cache.RemoveAsync($"azkar:{request.Category}");

            return Ok(ApiResponse<ZikrDto>.Ok(new ZikrDto 
            {
                Id=zikr.Id,
                TextArabic=zikr.TextArabic,
                TextTransliteration=zikr.TextTransliteration,
                Meaning=zikr.Meaning,
                Source=zikr.Source,
                RepeatCount=zikr.RepeatCount,
                Category= zikr.Category.ToString()


            
            }, "Zikr created successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZikr(int id)
        {
            var zikr = await _unitOfWork.Azkar.GetByIdAsync(id);
            if (zikr == null) return NotFound(ApiResponse<string>.Fail("Zikr not found"));

            await _cache.RemoveAsync($"azkar:{zikr.Category}");

            await _unitOfWork.Azkar.DeleteAsync(zikr);
            await _unitOfWork.SaveChangesAsync();

            return Ok(ApiResponse<string>.Ok("Deleted"));
        }
    }

   
}
