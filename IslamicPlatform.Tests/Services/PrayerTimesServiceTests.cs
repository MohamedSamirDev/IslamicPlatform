using FluentAssertions;
using IslamicPlatform.Application.DTOs.PrayerTimesDTOs;
using IslamicPlatform.Application.Interfaces.Services;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IslamicPlatform.Tests.Services
{
    // IslamicPlatform.Tests/Services/PrayerTimesServiceTests.cs
    public class PrayerTimesServiceTests
    {
        private readonly Mock<ICacheService> _cacheMock;
        private readonly Mock<HttpMessageHandler> _httpHandlerMock;
        private readonly PrayerTimesService _prayerTimesService;

        public PrayerTimesServiceTests()
        {
            _cacheMock = new Mock<ICacheService>();
            _httpHandlerMock = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(_httpHandlerMock.Object);
            _prayerTimesService = new PrayerTimesService(httpClient, _cacheMock.Object);
        }

        [Fact]
        public async Task GetPrayerTimes_ShouldReturnFromCache_WhenCached()
        {
            // Arrange
            var cached = new PrayerTimesDTO
            {
                Fajr = "04:30",
                Dhuhr = "12:00",
                Asr = "15:30",
                Maghrib = "18:00",
                Isha = "19:30"
            };

            _cacheMock.Setup(c => c.GetAsync<PrayerTimesDTO>(It.IsAny<string>()))
                .ReturnsAsync(cached);

            // Act
            var result = await _prayerTimesService.GetPrayerTimesAsync(30.04, 31.23);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.Fajr.Should().Be("04:30");

            // التأكد إننا ماكلمناش الـ API لأن البيانات جت من الـ Cache
            _httpHandlerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetPrayerTimes_ShouldCallApi_WhenNotCached()
        {
            // Arrange
            _cacheMock.Setup(c => c.GetAsync<PrayerTimesDTO>(It.IsAny<string>()))
                .ReturnsAsync((PrayerTimesDTO?)null);

            var apiResponse = JsonSerializer.Serialize(new
            {
                data = new
                {
                    timings = new
                    {
                        Fajr = "04:30",
                        Sunrise = "06:00",
                        Dhuhr = "12:00",
                        Asr = "15:30",
                        Maghrib = "18:00",
                        Isha = "19:30"
                    },
                    date = new { readable = "01 Jan 2025" },
                    meta = new { timezone = "Africa/Cairo" }
                }
            });

            _httpHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(apiResponse)
                });

            // Act
            var result = await _prayerTimesService.GetPrayerTimesAsync(30.04, 31.23);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.Fajr.Should().Be("04:30");
            _cacheMock.Verify(c => c.SetAsync(It.IsAny<string>(), It.IsAny<PrayerTimesDTO>(), It.IsAny<TimeSpan>()), Times.Once);
        }

        [Fact]
        public async Task GetNextPrayer_ShouldReturnFajr_WhenAllPrayersPassed()
        {
            // Arrange — كل الصلوات فاتت
            var cached = new PrayerTimesDTO
            {
                Fajr = "04:00",
                Dhuhr = "11:00",
                Asr = "14:00",
                Maghrib = "17:00",
                Isha = "18:00"
            };

            _cacheMock.Setup(c => c.GetAsync<PrayerTimesDTO>(It.IsAny<string>()))
                .ReturnsAsync(cached);

            // Act — الساعة 23:00 يعني كل الصلوات فاتت
            var result = await _prayerTimesService.GetNextPrayerAsync(30.04, 31.23);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.PrayerName.Should().Be("Fajr");
            result.Data.RemainingTime.Should().Be("Tomorrow");
        }
    }
}
