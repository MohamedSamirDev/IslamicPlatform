using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrayerTimesController : ControllerBase
    {
        private readonly IPrayerTimesService _prayerTimesService;

        public PrayerTimesController(IPrayerTimesService prayerTimesService)=>_prayerTimesService= prayerTimesService;

        [HttpGet]
        public async Task<IActionResult> GetPrayerTimes([FromQuery] double latitude,[FromQuery] double longitude)
        {
            if (latitude == 0 && longitude == 0)
                return BadRequest(ApiResponse<string>.Fail("Invalid Location"));
            var result= await _prayerTimesService.GetPrayerTimesAsync(latitude, longitude);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("next")]
        public async Task<IActionResult> GetNextPrayer([FromQuery]double latitude,[FromQuery] double longitude)
        {
            if (latitude == 0 && longitude == 0)
                return BadRequest(ApiResponse<string>.Fail("Invalid Location"));
            var result= await _prayerTimesService.GetNextPrayerAsync(latitude, longitude);
            return result.Success ? Ok(result) : BadRequest(result);
        }
       
    }
}
