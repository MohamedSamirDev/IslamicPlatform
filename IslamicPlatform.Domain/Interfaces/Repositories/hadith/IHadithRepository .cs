using IslamicPlatform.Domain.Entites.hadith;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories.hadith
{
    public interface IHadithRepository : IGenericRepository<Hadith>
    {
        Task<IEnumerable<Hadith>> GetByBookAsync(int bookId);
        Task<IEnumerable<Hadith>> SearchAsync(string keyword);
       Task<IEnumerable<Hadith>> GetByChapterAsync(int chapterId);
        Task<IEnumerable<HadithBook>> GetAllBooksAsync();
        Task<IEnumerable<HadithChapter>> GetChaptersByBookAsync(int bookId);
        Task AddChapterAsync(HadithChapter chapter);
        Task AddBookAsync(HadithBook book);

        //PaginatedResult
        Task<(IEnumerable<Hadith> Data, int TotalCount)> GetByBookPagedAsync(int bookId, int page, int pageSize);
        Task<(IEnumerable<Hadith> Data, int TotalCount)> SearchPagedAsync(string keyword, int page, int pageSize);
    }
}
