using Aegis.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aegis.Api.Repositories.Interfaces
{
    public interface IChaserRepository
    {
        Task<IEnumerable<Chaser>> GetAllAsync();
        Task<Chaser?> GetByIdAsync(int id);
        Task AddAsync(Chaser chaser);
        Task UpdateAsync(Chaser chaser);
        Task DeleteAsync(int id);
    }
}