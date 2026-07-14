using IslamicPlatform.Domain.Entites.Azkar;
using IslamicPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories.Azkar
{
    public interface IZikrRepository : IGenericRepository<Zikr>
    {
        Task<IEnumerable<Zikr>> GetByCategoryAsync(ZikrCategory category);
        Task<UserZikrProgress?> GetUserProgressAsync(string userId, int zikrId);
        Task<IEnumerable<UserZikrProgress>> GetAllUserProgressAsync(string userId, ZikrCategory category);
        Task AddProgressAsync(UserZikrProgress progress);
        Task ResetDailyProgressAsync(string userId);
    }
}
