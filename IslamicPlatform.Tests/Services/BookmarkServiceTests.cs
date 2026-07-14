using FluentAssertions;
using IslamicPlatform.Application.Services;
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Entites.Quran;
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
    // IslamicPlatform.Tests/Services/BookmarkServiceTests.cs
    public class BookmarkServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly BookmarkService _bookmarkService;

        public BookmarkServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _bookmarkService = new BookmarkService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddBookmark_ShouldFail_WhenAlreadyBookmarked()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Bookmarks.IsBookmarkedAsync("user1", BookmarkType.Ayah, 1))
                .ReturnsAsync(true);

            // Act
            var result = await _bookmarkService.AddBookmarkAsync("user1", BookmarkType.Ayah, 1);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Already bookmarked");
        }

        [Fact]
        public async Task AddBookmark_ShouldSucceed_WhenNotBookmarkedBefore()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Bookmarks.IsBookmarkedAsync("user1", BookmarkType.Ayah, 1))
                .ReturnsAsync(false);

            _unitOfWorkMock.Setup(u => u.Bookmarks.AddAsync(It.IsAny<Bookmark>()))
                .ReturnsAsync(new Bookmark { Id = 1, Type = BookmarkType.Ayah });

            // Act
            var result = await _bookmarkService.AddBookmarkAsync("user1", BookmarkType.Ayah, 1);

            // Assert
            result.Success.Should().BeTrue();
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveBookmark_ShouldFail_WhenNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Bookmarks.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Bookmark?)null);

            // Act
            var result = await _bookmarkService.RemoveBookmarkAsync("user1", 999);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Bookmark not found");
        }

        [Fact]
        public async Task RemoveBookmark_ShouldFail_WhenBookmarkBelongsToOtherUser()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Bookmarks.GetByIdAsync(1))
                .ReturnsAsync(new Bookmark { Id = 1, UserId = "other_user" });

            // Act
            var result = await _bookmarkService.RemoveBookmarkAsync("user1", 1);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Unauthorized");
        }

        [Fact]
        public async Task RemoveBookmark_ShouldSucceed_WhenValidRequest()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Bookmarks.GetByIdAsync(1))
                .ReturnsAsync(new Bookmark { Id = 1, UserId = "user1" });

            // Act
            var result = await _bookmarkService.RemoveBookmarkAsync("user1", 1);

            // Assert
            result.Success.Should().BeTrue();
            _unitOfWorkMock.Verify(u => u.Bookmarks.DeleteAsync(It.IsAny<Bookmark>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetUserBookmarks_ShouldReturnBookmarks()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Bookmarks.GetUserBookmarksAsync("user1"))
                .ReturnsAsync(new List<Bookmark>
                {
                new() { Id = 1, UserId = "user1", Type = BookmarkType.Ayah,
                    Ayah = new Ayah { Id = 1, TextArabic = "بسم الله الرحمن الرحيم", NumberInSurah = 1 } },
                new() { Id = 2, UserId = "user1", Type = BookmarkType.Hadith,
                    Hadith = new Hadith { Id = 1, TextArabic = "إنما الأعمال بالنيات" } }
                });

            // Act
            var result = await _bookmarkService.GetUserBookmarksAsync("user1");

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
        }
    }
}
