using FluentAssertions;
using IslamicPlatform.Application.DTOs.CloudinaryUploadResultDTOs;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Application.Services;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Entites.Sheikh;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace IslamicPlatform.Tests.Services
{
    public class AudioServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICloudinaryService> _cloudinaryMock;
        private readonly AudioService _audioService;

        public AudioServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _cloudinaryMock = new Mock<ICloudinaryService>();
            _audioService = new AudioService(_unitOfWorkMock.Object, _cloudinaryMock.Object);
        }

        [Fact]
        public async Task GetAllSheikhs_ShouldReturnAllSheikhs()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Sheikhs.GetAllAsync())
                .ReturnsAsync(new List<Sheikh>
                {
                new() { Id = 1, NameArabic = "مشاري العفاسي", NameEnglish = "Alafasy", Country = "الكويت" },
                new() { Id = 2, NameArabic = "عبد الباسط", NameEnglish = "Abdul Basit", Country = "مصر" }
                });

            // Act
            var result = await _audioService.GetAllSheikhsAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetRecitation_ShouldFail_WhenNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Sheikhs.GetRecitationAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((Recitation?)null);

            // Act
            var result = await _audioService.GetRecitationAsync(1, 1);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Recitation not found");
        }

        [Fact]
        public async Task GetRecitation_ShouldReturnRecitation_WhenFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Sheikhs.GetRecitationAsync(1, 1))
                .ReturnsAsync(new Recitation { Id = 1, AudioUrl = "https://mp3.com/001.mp3", SheikhId = 1, SurahId = 1 });

            _unitOfWorkMock.Setup(u => u.Sheikhs.GetByIdAsync(1))
                .ReturnsAsync(new Sheikh { Id = 1, NameArabic = "العفاسي", NameEnglish = "Alafasy" });

            _unitOfWorkMock.Setup(u => u.Surahs.GetByIdAsync(1))
                .ReturnsAsync(new Surah { Id = 1, Number = 1, NameArabic = "الفاتحة", NameEnglish = "Al-Fatihah" });

            // Act
            var result = await _audioService.GetRecitationAsync(1, 1);

            // Assert
         

            result.Should().NotBeNull();

            result.Success.Should().BeTrue();

            result.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSheikhRecitations_ShouldFail_WhenSheikhNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Sheikhs.GetWithRecitationsAsync(It.IsAny<int>()))
                .ReturnsAsync((Sheikh?)null);

            // Act
            var result = await _audioService.GetSheikhRecitationsAsync(999);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Sheikh not found");
        }

        [Fact]
        public async Task UploadSheikhImage_ShouldFail_WhenSheikhNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Sheikhs.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Sheikh?)null);

            var fileMock = new Mock<IFormFile>();

            // Act
            var result = await _audioService.UploadSheikhImageAsync(999, fileMock.Object);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Sheikh not found");
        }

        [Fact]
        public async Task UploadSheikhImage_ShouldDeleteOldImage_WhenSheikhHasExistingImage()
        {
            // Arrange
            var sheikh = new Sheikh { Id = 1, NameArabic = "العفاسي", ImagePublicId = "old_public_id" };

            _unitOfWorkMock.Setup(u => u.Sheikhs.GetByIdAsync(1)).ReturnsAsync(sheikh);
            _cloudinaryMock.Setup(c => c.DeleteImageAsync("old_public_id")).ReturnsAsync(true);
            _cloudinaryMock.Setup(c => c.UploadImageAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(new CloudinaryUploadResult { Url = "https://new.url", PublicId = "new_id" });

            var fileMock = new Mock<IFormFile>();

            // Act
            await _audioService.UploadSheikhImageAsync(1, fileMock.Object);

            // Assert
            _cloudinaryMock.Verify(c => c.DeleteImageAsync("old_public_id"), Times.Once);
        }

    }
}