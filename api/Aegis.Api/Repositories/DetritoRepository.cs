using Microsoft.EntityFrameworkCore;
using Aegis.Api.Data;
using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;

namespace Aegis.Api.Repositories
{
    public class DetritoRepository : IDetritoRepository
    {
        private readonly AppDbContext _context;

        public DetritoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<DetritoEspacial>> GetAllAsync()
        {
            return await _context.Detritos
                .AsNoTracking()
                .Include(d => d.Alertas) // Carrega os alertas relacionados
                .ToListAsync();
        }

        public async Task<DetritoEspacial?> GetByIdAsync(int id)
        {
            return await _context.Detritos
                .AsNoTracking()
                .Include(d => d.Alertas)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(DetritoEspacial detrito)
        {
            await _context.Detritos.AddAsync(detrito);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DetritoEspacial detrito)
        {
            _context.Detritos.Update(detrito);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var detrito = await _context.Detritos.FindAsync(id);
            if (detrito != null)
            {
                _context.Detritos.Remove(detrito);
                await _context.SaveChangesAsync();
            }
        }
    }
}