using IslamicPlatform.Application.Helpers;
using IslamicPlatform.Domain.Entites.Quran;
using IslamicPlatform.Domain.Interfaces.Repositories.Quran;
using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Repositorys.Quran
{
    public class SurahRepository : GenericRepository<Surah>, ISurahRepository
    {
        public SurahRepository(ApplicationDbContext context):base(context) { }
        public async Task<Surah?> GetByNumberAsync(int number)
         => await
            _dbSet.FirstOrDefaultAsync(a=>a.Number== number);

        public async Task<Surah?> GetWithAyahsAsync(int surahId)
            => await
                 _dbSet.Include(s => s.Ayahs)
                       .FirstOrDefaultAsync(s => s.Id == surahId);

        public async Task<IEnumerable<Surah>> SearchAsync(string keyword)
        {
            var normalizedKeyword = ArabicSearchHelper.NormalizeArabic(keyword);

            return await _dbSet
                .Where(s =>
                    s.NameArabicNormalized!.Contains(normalizedKeyword) ||
                    s.NameEnglish.Contains(keyword))
                .ToListAsync();
        }

    }
}
