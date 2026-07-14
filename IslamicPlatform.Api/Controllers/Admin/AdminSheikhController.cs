using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AudioDTOs;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Entites.Sheikh;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IslamicPlatform.Api.Controllers.Admin
{
  
    [ApiController]
    [Route("api/admin/sheikhs")]
    [Authorize(Roles = "Admin")]
    public class AdminSheikhController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;

        public AdminSheikhController(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSheikh([FromForm] CreateSheikhRequest request)
        {
            var sheikh = new Sheikh
            {
                NameArabic = request.NameArabic,
                NameEnglish = request.NameEnglish,
                Country = request.Country,
                Bio = request.Bio,
                MoshafType = request.MoshafType
            };

            if (request.Image != null)
            {
                var result = await _cloudinaryService.UploadImageAsync(request.Image, "sheikhs");
                sheikh.ImageUrl = result.Url;
                sheikh.ImagePublicId = result.PublicId;
            }

            await _unitOfWork.Sheikhs.AddAsync(sheikh);
            await _unitOfWork.SaveChangesAsync();

            return Ok(ApiResponse<SheikhDto>.Ok(new SheikhDto
            {
                Id=sheikh.Id,
                NameArabic=sheikh.NameArabic,
                NameEnglish=sheikh.NameEnglish,
                Bio=sheikh.Bio,
                ImageUrl=sheikh.ImageUrl,
                MoshafType=sheikh.MoshafType
            },"Sheikh Created Successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSheikh(int id, [FromForm] UpdateSheikhRequest request)
        {
            var sheikh = await _unitOfWork.Sheikhs.GetByIdAsync(id);
            if (sheikh == null) return NotFound(ApiResponse<string>.Fail("Sheikh not found"));

            sheikh.NameArabic = !string.IsNullOrWhiteSpace(request.NameArabic)
                 ? request.NameArabic : sheikh.NameArabic;
            sheikh.NameEnglish = !string.IsNullOrWhiteSpace(request.NameEnglish)
                ? request.NameEnglish : sheikh.NameEnglish;
            sheikh.Country = !string.IsNullOrWhiteSpace(request.Country)
                ? request.Country : sheikh.Country;
            sheikh.Bio = !string.IsNullOrWhiteSpace(request.Bio)
                ? request.Bio : sheikh.Bio;

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(sheikh.ImagePublicId))
                    await _cloudinaryService.DeleteImageAsync(sheikh.ImagePublicId);

                var result = await _cloudinaryService.UploadImageAsync(request.Image, "sheikhs");
                sheikh.ImageUrl = result.Url;
                sheikh.ImagePublicId = result.PublicId;
            }

            await _unitOfWork.Sheikhs.UpdateAsync(sheikh);
            await _unitOfWork.SaveChangesAsync();

            return Ok(ApiResponse<string>.Ok("Updated", "Sheikh updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSheikh(int id)
        {
            var sheikh = await _unitOfWork.Sheikhs.GetByIdAsync(id);
            if (sheikh == null) return NotFound(ApiResponse<string>.Fail("Sheikh not found"));

            if (!string.IsNullOrEmpty(sheikh.ImagePublicId))
                await _cloudinaryService.DeleteImageAsync(sheikh.ImagePublicId);

            await _unitOfWork.Sheikhs.DeleteAsync(sheikh);
            await _unitOfWork.SaveChangesAsync();

            return Ok(ApiResponse<string>.Ok("Deleted", "Sheikh deleted successfully"));
        }
    }

    
  

   
}
