using IslamicPlatform.Domain.Interfaces.Repositories;
using IslamicPlatform.Domain.Interfaces.Repositories.Audio;
using IslamicPlatform.Domain.Interfaces.Repositories.Azkar;
using IslamicPlatform.Domain.Interfaces.Repositories.hadith;
using IslamicPlatform.Domain.Interfaces.Repositories.Identity;
using IslamicPlatform.Domain.Interfaces.Repositories.Quran;
using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Repositorys.Audio;
using IslamicPlatform.Infrastructure.Repositorys.Azkar;
using IslamicPlatform.Infrastructure.Repositorys.hadith;
using IslamicPlatform.Infrastructure.Repositorys.Identity;
using IslamicPlatform.Infrastructure.Repositorys.Quran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Repositorys
{
    // IslamicPlatform.Infrastructure/Repositories/UnitOfWork.cs
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ISurahRepository Surahs { get; }
        public IAyahRepository Ayahs { get; }
        public ISheikhRepository Sheikhs { get; }
        public IZikrRepository Azkar { get; }
        public IHadithRepository Hadiths { get; }
        public IBookmarkRepository Bookmarks { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public IReadingProgressRepository ReadingProgresses { get; }

        

        public UnitOfWork(
            ApplicationDbContext context,
            ISurahRepository surahs,
            IAyahRepository ayahs,
            ISheikhRepository sheikhs,
            IZikrRepository azkar,
            IHadithRepository hadiths,
            IBookmarkRepository bookmarks,
            IRefreshTokenRepository refreshTokens,
            IReadingProgressRepository readingProgresses
           )
        {
            _context = context;
            Surahs = surahs;
            Ayahs = ayahs;
            Sheikhs = sheikhs;
            Azkar = azkar;
            Hadiths = hadiths;
            Bookmarks = bookmarks;
            RefreshTokens = refreshTokens;
            ReadingProgresses = readingProgresses;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

       
    }
}
