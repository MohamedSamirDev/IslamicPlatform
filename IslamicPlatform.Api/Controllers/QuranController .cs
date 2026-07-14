// IslamicPlatform.API/Controllers/QuranController.cs
using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class QuranController : ControllerBase
{
    private readonly IQuranService _quranService;

    public QuranController(IQuranService quranService)
        => _quranService = quranService;

    // GET /api/quran/surahs
    [HttpGet("surahs")]
    public async Task<IActionResult> GetAllSurahs()
    {
        var result = await _quranService.GetAllSurahsAsync();
        return Ok(result);
    }

    // GET /api/quran/surahs/{number}
    [HttpGet("surahs/{number}")]
    public async Task<IActionResult> GetSurahByNumber(int number)
    {
        var result = await _quranService.GetSurahByNumberAsync(number);
        return result.Success ? Ok(result) : NotFound(result);
    }

    // GET /api/quran/surahs/{surahId}/ayahs
    [HttpGet("surahs/{surahId}/ayahs")]
    public async Task<IActionResult> GetAyahsBySurah(int surahId)
    {
        var result = await _quranService.GetAyahsBySurahAsync(surahId);
        return result.Success ? Ok(result) : NotFound(result);
    }

    // GET /api/quran/ayahs/{ayahId}/tafseer
    [HttpGet("ayahs/{ayahId}/tafseer")]
    public async Task<IActionResult> GetAyahWithTafseer(int ayahId)
    {
        var result = await _quranService.GetAyahWithTafseerAsync(ayahId);
        return result.Success ? Ok(result) : NotFound(result);
    }

    // GET /api/quran/juz/{number}
    [HttpGet("juz/{number}")]
    public async Task<IActionResult> GetByJuz(int number)
    {
        var result = await _quranService.GetByJuzAsync(number);
        return result.Success ? Ok(result) : NotFound(result);
    }

    // GET /api/quran/search?keyword=...
    [HttpGet("search")]
    public async Task<IActionResult> SearchAyahs([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return BadRequest(ApiResponse<string>.Fail("Keyword is required"));

        var result = await _quranService.SearchAyahsAsync(keyword);
        return Ok(result);
    }

    // GET /api/quran/search/surahs?keyword=...
    [HttpGet("search/surahs")]
    public async Task<IActionResult> SearchSurahs([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return BadRequest(ApiResponse<string>.Fail("Keyword is required"));

        var result = await _quranService.SearchSurahsAsync(keyword);
        return Ok(result);
    }
}