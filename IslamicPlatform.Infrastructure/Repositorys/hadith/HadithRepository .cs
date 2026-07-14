using IslamicPlatform.Application.Helpers;
using IslamicPlatform.Domain.Entites.hadith;
using IslamicPlatform.Domain.Interfaces.Repositories.hadith;
using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Repositorys.hadith
{
    public class HadithRepository : GenericRepository<Hadith>, IHadithRepository
    {
        public HadithRepository(ApplicationDbContext context) : base(context) { }


        public async Task<IEnumerable<Hadith>> GetByBookAsync(int bookId)
        => await _dbSet.Include(s => s.Chapter)
                .ThenInclude(c => c.Book)
                .Where(s => s.Chapter.BookId == bookId)
                .ToListAsync();
        public async Task<IEnumerable<Hadith>> SearchAsync(string keyword)
        {
            keyword = ArabicSearchHelper.NormalizeArabic(keyword);

            return await _dbSet
                .Where(s =>
                    s.TextArabicSearch.Contains(keyword)
                    || s.TextEnglish.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<HadithBook>> GetAllBooksAsync()
        => await
            _context.HadithBooks.ToListAsync();


        public async Task<IEnumerable<Hadith>> GetByChapterAsync(int chapterId)
        => await _dbSet
            .Include(s => s.Chapter)
            .ThenInclude(c => c.Book)  
            .Where(s => s.ChapterId == chapterId)
           .OrderBy(s => s.Number)
           .ToListAsync();
        public async Task AddBookAsync(HadithBook book)
        {
             await _context.HadithBooks.AddAsync(book);
        }
        public async Task AddChapterAsync(HadithChapter chapter)
        {
             await _context.HadithChapters.AddAsync(chapter);
        }


        public async Task<IEnumerable<HadithChapter>> GetChaptersByBookAsync(int bookId)
        => await _context.HadithChapters
         .Include(c => c.Book)  // ← ضيف ده
         .Where(s => s.BookId == bookId)
         .ToListAsync();

        public async Task<(IEnumerable<Hadith> Data, int TotalCount)> GetByBookPagedAsync(int bookId, int page, int pageSize)
        {
            var query = _dbSet.Include(p => p.Chapter)
                  .ThenInclude(c => c.Book)
                 .Where(p => p.Chapter.BookId == bookId);

            var total = await query.CountAsync();

            var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
             .ToListAsync();

            return (data, total);



        }

        public async Task<(IEnumerable<Hadith> Data, int TotalCount)> SearchPagedAsync(string keyword, int page, int pageSize)
        {
            keyword = ArabicSearchHelper.NormalizeArabic(keyword);

            var query = _dbSet.Where(p =>
                   p.TextArabicSearch.Contains(keyword)
                || p.TextEnglish.Contains(keyword));

            var total = await query.CountAsync();

            var data = await query
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();

            return (data, total);
        }
       


    }
}
