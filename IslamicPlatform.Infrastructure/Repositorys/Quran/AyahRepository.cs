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
    public class AyahRepository : GenericRepository<Ayah>, IAyahRepository
    {
        public AyahRepository(ApplicationDbContext context):base(context) { }

        public async Task<IEnumerable<Ayah>> GetByJuzAsync(int juzNumber)
         => await _dbSet
       .Include(a => a.Translations)
       .Include(a => a.Tafseers)
       .Where(a => a.JuzNumber == juzNumber)
       .OrderBy(a => a.Number)
       .ToListAsync();

        public async Task<IEnumerable<Ayah>> GetBySurahAsync(int surahId)
         => await _dbSet
        .Include(a => a.Translations)
        .Include(a => a.Tafseers)
        .Where(a => a.SurahId == surahId)
        .OrderBy(a => a.NumberInSurah)
        .ToListAsync();

        public async Task<Ayah?> GetWithTafseerAsync(int ayahId)
        => await
            _dbSet.Include(a => a.Tafseers)
            .FirstOrDefaultAsync(a => a.Id == ayahId);

        public async Task<Ayah?> GetWithTranslationsAsync(int ayahId)
        => await
            _dbSet.Include(a => a.Translations)
            .FirstOrDefaultAsync(a => a.Id == ayahId);


        public async Task<IEnumerable<Ayah>> SearchAsync(string keyword)
        {
            var normalizedKeyword = ArabicSearchHelper.NormalizeArabic(keyword);

            var all = await _dbSet
                .Include(a => a.Translations)
                .Include(a => a.Tafseers)
                .ToListAsync();

            return all.Where(a =>
                (a.TextArabicNormalized ?? a.TextArabic).Contains(normalizedKeyword));
        }
    }
}
