using Aegis.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aegis.Api.Repositories.Interfaces
{
    public interface IMissaoRepository
    {
        Task<IEnumerable<MissaoInterceptacao>> GetAllAsync();
        Task<MissaoInterceptacao?> GetByIdAsync(int id);
        Task AddAsync(MissaoInterceptacao missao);
        Task UpdateAsync(MissaoInterceptacao missao);
        Task DeleteAsync(int id);
    }
}