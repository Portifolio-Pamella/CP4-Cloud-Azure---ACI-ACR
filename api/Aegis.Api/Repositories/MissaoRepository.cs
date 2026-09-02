using Microsoft.EntityFrameworkCore;
using Aegis.Api.Data;
using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;

namespace Aegis.Api.Repositories
{
    public class MissaoRepository : IMissaoRepository
    {
        private readonly AppDbContext _context;

        public MissaoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<MissaoInterceptacao>> GetAllAsync()
        {
            return await _context.Missoes
                .AsNoTracking()
                .Include(m => m.Alerta)
                .Include(m => m.Chaser)
                .ToListAsync();
        }

        public async Task<MissaoInterceptacao?> GetByIdAsync(int id)
        {
            return await _context.Missoes
                .AsNoTracking()
                .Include(m => m.Alerta)
                .Include(m => m.Chaser)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(MissaoInterceptacao missao)
        {
            // Limpa as propriedades de navegação para evitar erros de duplicidade no EF
            missao.Alerta = null;
            missao.Chaser = null;
            await _context.Missoes.AddAsync(missao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MissaoInterceptacao missao)
        {
            _context.Missoes.Update(missao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var missao = await _context.Missoes.FindAsync(id);
            if (missao != null)
            {
                _context.Missoes.Remove(missao);
                await _context.SaveChangesAsync();
            }
        }
    }
}