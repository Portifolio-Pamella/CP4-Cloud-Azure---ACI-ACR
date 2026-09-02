using Aegis.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aegis.Api.Repositories.Interfaces
{
    public interface ISateliteRepository
    {
        Task<IEnumerable<Satelite>> GetAllAsync();
        Task<Satelite?> GetByIdAsync(int id);
        Task AddAsync(Satelite satelite);
        Task UpdateAsync(Satelite satelite);
        Task DeleteAsync(int id);
    }
}