using IslamicPlatform.Domain.Entites.Sheikh;
using IslamicPlatform.Domain.Interfaces.Repositories.Audio;
using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Repositorys.Audio
{
    public class SheikhRepository : GenericRepository<Sheikh>, ISheikhRepository
    {
        public SheikhRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Sheikh>> GetAllWithRecitationsAsync()
        => await
            _dbSet.Include(a => a.Recitations)
            .ThenInclude(r => r.Surah)
            .ToListAsync();

        public async Task<Sheikh?> GetWithRecitationsAsync(int sheikhId)
        => await
            _dbSet.Include(a => a.Recitations)
             .ThenInclude(r => r.Surah)
            .FirstOrDefaultAsync(s => s.Id == sheikhId);

        public async Task<Recitation?> GetRecitationAsync(int sheikhId, int surahId)
        => await
            _context.Recitations
            .Include(r=>r.Sheikh)
            .Include(r=>r.Surah)
            .FirstOrDefaultAsync(s => s.SheikhId == sheikhId && s.SurahId == surahId);

    }
}
