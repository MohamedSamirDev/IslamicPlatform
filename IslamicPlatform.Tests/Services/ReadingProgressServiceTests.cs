using FluentAssertions;
using IslamicPlatform.Application.Services;
using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Tests.Services
{
    // IslamicPlatform.Tests/Services/ReadingProgressServiceTests.cs
    public class ReadingProgressServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ReadingProgressService _readingProgressService;

        public ReadingProgressServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _readingProgressService = new ReadingProgressService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetProgress_ShouldFail_WhenNoProgressFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.ReadingProgresses.GetByUserIdAsync("user1"))
                .ReturnsAsync((ReadingProgress?)null);

            // Act
            var result = await _readingProgressService.GetProgressAsync("user1");

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("No reading progress found");
        }

        [Fact]
        public async Task GetProgress_ShouldReturnProgress_WhenFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.ReadingProgresses.GetByUserIdAsync("user1"))
                .ReturnsAsync(new ReadingProgress { UserId = "user1", LastSurahId = 2, LastAyahId = 255 });

            _unitOfWorkMock.Setup(u => u.Surahs.GetByIdAsync(2))
                .ReturnsAsync(new Surah { Id = 2, NameArabic = "البقرة" });

            _unitOfWorkMock.Setup(u => u.Ayahs.GetByIdAsync(255))
                .ReturnsAsync(new Ayah { Id = 255, NumberInSurah = 255 });

            // Act
            var result = await _readingProgressService.GetProgressAsync("user1");

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.LastSurahName.Should().Be("البقرة");
            result.Data.LastAyahNumber.Should().Be(255);
        }

        [Fact]
        public async Task UpdateProgress_ShouldFail_WhenSurahNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Surahs.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Surah?)null);

            // Act
            var result = await _readingProgressService.UpdateProgressAsync("user1", 999, 1);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Surah not found");
        }

        [Fact]
        public async Task UpdateProgress_ShouldFail_WhenAyahNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Surahs.GetByIdAsync(1))
                .ReturnsAsync(new Surah { Id = 1, NameArabic = "الفاتحة" });

            _unitOfWorkMock.Setup(u => u.Ayahs.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Ayah?)null);

            // Act
            var result = await _readingProgressService.UpdateProgressAsync("user1", 1, 999);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Ayah not found");
        }

        [Fact]
        public async Task UpdateProgress_ShouldCreateNew_WhenFirstTime()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Surahs.GetByIdAsync(1))
                .ReturnsAsync(new Surah { Id = 1, NameArabic = "الفاتحة" });

            _unitOfWorkMock.Setup(u => u.Ayahs.GetByIdAsync(1))
                .ReturnsAsync(new Ayah { Id = 1, NumberInSurah = 1 });

            _unitOfWorkMock.Setup(u => u.ReadingProgresses.GetByUserIdAsync("user1"))
                .ReturnsAsync((ReadingProgress?)null);

            // Act
            var result = await _readingProgressService.UpdateProgressAsync("user1", 1, 1);

            // Assert
            result.Success.Should().BeTrue();
            _unitOfWorkMock.Verify(u => u.ReadingProgresses.AddAsync(It.IsAny<ReadingProgress>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProgress_ShouldUpdate_WhenProgressExists()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Surahs.GetByIdAsync(2))
                .ReturnsAsync(new Surah { Id = 2, NameArabic = "البقرة" });

            _unitOfWorkMock.Setup(u => u.Ayahs.GetByIdAsync(10))
                .ReturnsAsync(new Ayah { Id = 10, NumberInSurah = 10 });

            _unitOfWorkMock.Setup(u => u.ReadingProgresses.GetByUserIdAsync("user1"))
                .ReturnsAsync(new ReadingProgress { UserId = "user1", LastSurahId = 1, LastAyahId = 1 });

            // Act
            var result = await _readingProgressService.UpdateProgressAsync("user1", 2, 10);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.LastSurahName.Should().Be("البقرة");
            _unitOfWorkMock.Verify(u => u.ReadingProgresses.UpdateAsync(It.IsAny<ReadingProgress>()), Times.Once);
        }
    }
}
