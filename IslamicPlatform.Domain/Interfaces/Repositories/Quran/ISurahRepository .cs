using IslamicPlatform.Domain.Entites.Quran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories.Quran
{
    public interface ISurahRepository:IGenericRepository<Surah>
    {
        Task<Surah?> GetByNumberAsync(int number);

        Task<Surah?> GetWithAyahsAsync(int surahId);

        Task<IEnumerable<Surah>> SearchAsync(string keyword);
    }
}
