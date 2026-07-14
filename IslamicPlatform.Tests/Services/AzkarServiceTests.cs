using FluentAssertions;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Application.Services;
using IslamicPlatform.Domain.Entites.Azkar;
using IslamicPlatform.Domain.Enums;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Tests.Services
{
    // IslamicPlatform.Tests/Services/AzkarServiceTests.cs
    public class AzkarServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICacheService> _cacheMock;
        private readonly AzkarService _azkarService;

        public AzkarServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _cacheMock = new Mock<ICacheService>();
            _azkarService = new AzkarService(_unitOfWorkMock.Object, _cacheMock.Object);
        }

        [Fact]
        public async Task GetByCategory_ShouldReturnAzkar_ForMorningCategory()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Azkar.GetByCategoryAsync(ZikrCategory.Morning))
                .ReturnsAsync(new List<Zikr>
                {
                new() { Id = 1, TextArabic = "سبحان الله", RepeatCount = 33, Category = ZikrCategory.Morning },
                new() { Id = 2, TextArabic = "الحمد لله", RepeatCount = 33, Category = ZikrCategory.Morning }
                });

            // Act
            var result = await _azkarService.GetByCategoryAsync(ZikrCategory.Morning);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
        }

        [Fact]
        public async Task IncrementCount_ShouldFail_WhenZikrNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Azkar.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Zikr?)null);

            // Act
            var result = await _azkarService.IncrementCountAsync("user1", 999);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Zikr not found");
        }

        [Fact]
        public async Task IncrementCount_ShouldCreateNewProgress_WhenFirstTime()
        {
            // Arrange
            var zikr = new Zikr { Id = 1, TextArabic = "سبحان الله", RepeatCount = 33 };

            _unitOfWorkMock.Setup(u => u.Azkar.GetByIdAsync(1)).ReturnsAsync(zikr);
            _unitOfWorkMock.Setup(u => u.Azkar.GetUserProgressAsync("user1", 1))
                .ReturnsAsync((UserZikrProgress?)null);

            // Act
            var result = await _azkarService.IncrementCountAsync("user1", 1);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.CurrentCount.Should().Be(1);
            result.Data.IsCompleted.Should().BeFalse();
        }

        [Fact]
        public async Task IncrementCount_ShouldMarkCompleted_WhenCountReachesMax()
        {
            // Arrange
            var zikr = new Zikr { Id = 1, TextArabic = "سبحان الله", RepeatCount = 3 };
            var progress = new UserZikrProgress
            {
                UserId = "user1",
                ZikrId = 1,
                CurrentCount = 2,
                IsCompleted = false
            };

            _unitOfWorkMock.Setup(u => u.Azkar.GetByIdAsync(1)).ReturnsAsync(zikr);
            _unitOfWorkMock.Setup(u => u.Azkar.GetUserProgressAsync("user1", 1)).ReturnsAsync(progress);

            // Act
            var result = await _azkarService.IncrementCountAsync("user1", 1);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.CurrentCount.Should().Be(3);
            result.Data.IsCompleted.Should().BeTrue();
        }

        [Fact]
        public async Task IncrementCount_ShouldNotIncrement_WhenAlreadyCompleted()
        {
            // Arrange
            var zikr = new Zikr { Id = 1, TextArabic = "سبحان الله", RepeatCount = 33 };
            var progress = new UserZikrProgress
            {
                UserId = "user1",
                ZikrId = 1,
                CurrentCount = 33,
                IsCompleted = true
            };

            _unitOfWorkMock.Setup(u => u.Azkar.GetByIdAsync(1)).ReturnsAsync(zikr);
            _unitOfWorkMock.Setup(u => u.Azkar.GetUserProgressAsync("user1", 1)).ReturnsAsync(progress);

            // Act
            var result = await _azkarService.IncrementCountAsync("user1", 1);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.CurrentCount.Should().Be(33); // مش اتزاد
            result.Data.IsCompleted.Should().BeTrue();
        }

        [Fact]
        public async Task ResetDailyProgress_ShouldReturnSuccess()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Azkar.ResetDailyProgressAsync("user1"))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _azkarService.ResetDailyProgressAsync("user1");

            // Assert
            result.Success.Should().BeTrue();
            _unitOfWorkMock.Verify(u => u.Azkar.ResetDailyProgressAsync("user1"), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
