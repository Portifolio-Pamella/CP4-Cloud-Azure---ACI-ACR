using Aegis.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aegis.Api.Services.Interfaces
{
    public interface IChaserService
    {
        Task<IEnumerable<Chaser>> GetAllAsync();
        Task<Chaser?> GetByIdAsync(int id);
        Task AddAsync(Chaser chaser);
        Task UpdateAsync(Chaser chaser);
        Task DeleteAsync(int id);
    }
}