using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Interfaces.Repositories.Identity;
using IslamicPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Repositorys.Identity
{
    public class ReadingProgressRepository : IReadingProgressRepository
    {
        private readonly ApplicationDbContext _context;

        public ReadingProgressRepository(ApplicationDbContext context)
          =>_context = context;
        

        public async Task<ReadingProgress?> GetByUserIdAsync(string userId)
         => await _context.ReadingProgresses.FirstOrDefaultAsync(rp => rp.UserId == userId);

        public async Task AddAsync(ReadingProgress progress)
        =>await 
            _context.ReadingProgresses.AddAsync(progress);


        public Task UpdateAsync(ReadingProgress progress)
        { 
            _context.ReadingProgresses.Update(progress);
            return Task.CompletedTask;
        }
    }
}
