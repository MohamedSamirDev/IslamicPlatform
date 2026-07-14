using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Interfaces.Repositories
{
    public interface IGenericRepository <T> where T : class
    {
        Task <IEnumerable<T>> GetAllAsync ();
        Task <T> GetByIdAsync (int id);

        Task <T> AddAsync(T entity);

        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(int id);
    }
}
