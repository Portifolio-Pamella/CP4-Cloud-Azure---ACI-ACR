using Microsoft.EntityFrameworkCore;
using Aegis.Api.Data;
using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;

namespace Aegis.Api.Repositories
{
    public class ChaserRepository : IChaserRepository
    {
        private readonly AppDbContext _context;

        public ChaserRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Chaser>> GetAllAsync()
        {
            return await _context.Chasers
                .AsNoTracking()
                .Include(c => c.Missoes) // Carrega as missões relacionadas
                .ToListAsync();
        }

        public async Task<Chaser?> GetByIdAsync(int id)
        {
            return await _context.Chasers
                .AsNoTracking()
                .Include(c => c.Missoes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Chaser chaser)
        {
            await _context.Chasers.AddAsync(chaser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chaser chaser)
        {
            _context.Chasers.Update(chaser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chaser = await _context.Chasers.FindAsync(id);
            if (chaser != null)
            {
                _context.Chasers.Remove(chaser);
                await _context.SaveChangesAsync();
            }
        }
    }
}