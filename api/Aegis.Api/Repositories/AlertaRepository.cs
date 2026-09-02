using Microsoft.EntityFrameworkCore;
using Aegis.Api.Data;
using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;

namespace Aegis.Api.Repositories
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly AppDbContext _context;

        public AlertaRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<AlertaColisao>> GetAllAsync()
        {
            return await _context.AlertasColisao
                .AsNoTracking()
                .Include(a => a.Satelite)
                .Include(a => a.Detrito)
                .ToListAsync();
        }

        public async Task<AlertaColisao?> GetByIdAsync(int id)
        {
            return await _context.AlertasColisao
                .AsNoTracking()
                .Include(a => a.Satelite)
                .Include(a => a.Detrito)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(AlertaColisao alerta)
        {
            // Limpa propriedades de navegação para evitar duplicações no EF
            alerta.Satelite = null;
            alerta.Detrito = null;

            await _context.AlertasColisao.AddAsync(alerta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AlertaColisao alerta)
        {
            _context.AlertasColisao.Update(alerta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var alerta = await _context.AlertasColisao.FindAsync(id);
            if (alerta != null)
            {
                _context.AlertasColisao.Remove(alerta);
                await _context.SaveChangesAsync();
            }
        }
    }
}