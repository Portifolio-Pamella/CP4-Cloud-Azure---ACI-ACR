using Aegis.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aegis.Api.Services.Interfaces
{
    public interface ISateliteService
    {
        Task<IEnumerable<Satelite>> GetAllAsync();
        Task<Satelite?> GetByIdAsync(int id);
        Task AddAsync(Satelite satelite);
        Task UpdateAsync(Satelite satelite);
        Task DeleteAsync(int id);
    }
}