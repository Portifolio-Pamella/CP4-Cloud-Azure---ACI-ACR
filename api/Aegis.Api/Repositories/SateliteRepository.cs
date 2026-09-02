using Microsoft.EntityFrameworkCore;
using Aegis.Api.Data;
using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;

namespace Aegis.Api.Repositories
{
    public class SateliteRepository : ISateliteRepository
    {
        private readonly AppDbContext _context;

        public SateliteRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Satelite>> GetAllAsync()
        {
            // AsNoTracking melhora a performance em consultas de leitura
            // Include traz a Empresa relacionada
            return await _context.Satelites
                .AsNoTracking()
                .Include(s => s.Empresa)
                .ToListAsync();
        }

        public async Task<Satelite?> GetByIdAsync(int id)
        {
            return await _context.Satelites
                .AsNoTracking()
                .Include(s => s.Empresa)
                .Include(s => s.Alertas) // Incluindo também os alertas associados
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Satelite satelite)
        {
            // Limpa propriedades de navegação para evitar duplicação ou erro no EF ao inserir
            satelite.Empresa = null;
            await _context.Satelites.AddAsync(satelite);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Satelite satelite)
        {
            _context.Satelites.Update(satelite);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var satelite = await _context.Satelites.FindAsync(id);
            if (satelite != null)
            {
                _context.Satelites.Remove(satelite);
                await _context.SaveChangesAsync();
            }
        }
    }
}