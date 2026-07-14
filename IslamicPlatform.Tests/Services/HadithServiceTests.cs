using FluentAssertions;
using IslamicPlatform.Application.Services;
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Tests.Services
{
    // IslamicPlatform.Tests/Services/HadithServiceTests.cs
    public class HadithServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly HadithService _hadithService;

        public HadithServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _hadithService = new HadithService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnAllBooks()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Hadiths.GetAllBooksAsync())
                .ReturnsAsync(new List<HadithBook>
                {
                new() { Id = 1, NameArabic = "صحيح البخاري", NameEnglish = "Sahih Al-Bukhari" },
                new() { Id = 2, NameArabic = "صحيح مسلم", NameEnglish = "Sahih Muslim" }
                });

            // Act
            var result = await _hadithService.GetAllBooksAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByBook_ShouldReturnHadiths_ForValidBook()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Hadiths.GetByBookAsync(1))
                .ReturnsAsync(new List<Hadith>
                {
                new() { Id = 1, Number = 1, TextArabic = "إنما الأعمال بالنيات", Grade = "صحيح",
                    Chapter = new HadithChapter { NameArabic = "باب النية", Book = new HadithBook { NameArabic = "البخاري" } } }
                });

            // Act
            var result = await _hadithService.GetByBookAsync(1);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
            result.Data!.First().TextArabic.Should().Be("إنما الأعمال بالنيات");
        }

        [Fact]
        public async Task Search_ShouldFail_WhenKeywordIsEmpty()
        {
            // Act
            var result = await _hadithService.SearchAsync("");

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Keyword is required");
        }

        [Fact]
        public async Task Search_ShouldReturnResults_WhenKeywordIsValid()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Hadiths.SearchAsync("النية"))
                .ReturnsAsync(new List<Hadith>
                {
                new() { Id = 1, Number = 1, TextArabic = "إنما الأعمال بالنيات",
                    Chapter = new HadithChapter { NameArabic = "باب النية", Book = new HadithBook { NameArabic = "البخاري" } } }
                });

            // Act
            var result = await _hadithService.SearchAsync("النية");

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
        }
    }
}
