using IslamicPlatform.Domain.Interfaces.Repositories.Audio;
using IslamicPlatform.Domain.Interfaces.Repositories.Azkar;
using IslamicPlatform.Domain.Interfaces.Repositories.hadith;
using IslamicPlatform.Domain.Interfaces.Repositories.Identity;
using IslamicPlatform.Domain.Interfaces.Repositories.Quran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ISurahRepository Surahs { get; }
        IAyahRepository Ayahs { get; }
        ISheikhRepository Sheikhs { get; }
        IZikrRepository Azkar { get; }
        IHadithRepository Hadiths { get; }
        IBookmarkRepository Bookmarks { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IReadingProgressRepository ReadingProgresses { get; }

        Task<int> SaveChangesAsync();
    }
}
