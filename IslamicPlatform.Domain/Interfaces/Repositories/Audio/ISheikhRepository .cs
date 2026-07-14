using IslamicPlatform.Domain.Entites.Sheikh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories.Audio
{
    public interface ISheikhRepository:IGenericRepository<Sheikh>
    {
        Task<IEnumerable<Sheikh>> GetAllWithRecitationsAsync();
        Task<Sheikh?> GetWithRecitationsAsync(int sheikhId);
        Task<Recitation?> GetRecitationAsync(int sheikhId, int surahId);
    }
}
