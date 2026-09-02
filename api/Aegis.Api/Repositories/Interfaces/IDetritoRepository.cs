using Aegis.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aegis.Api.Repositories.Interfaces
{
    public interface IDetritoRepository
    {
        Task<IEnumerable<DetritoEspacial>> GetAllAsync();
        Task<DetritoEspacial?> GetByIdAsync(int id);
        Task AddAsync(DetritoEspacial detrito);
        Task UpdateAsync(DetritoEspacial detrito);
        Task DeleteAsync(int id);
    }
}