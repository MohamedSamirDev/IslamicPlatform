using IslamicPlatform.Domain.Entites.Quran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories.Quran
{
    public interface IAyahRepository:IGenericRepository<Ayah>
    {
        Task<IEnumerable<Ayah>> GetBySurahAsync(int surahId);
        Task<Ayah?> GetWithTranslationsAsync(int ayahId);
        Task<Ayah?> GetWithTafseerAsync(int ayahId);
        Task<IEnumerable<Ayah>> GetByJuzAsync(int juzNumber);
        Task<IEnumerable<Ayah>> SearchAsync(string keyword);


    }
}
