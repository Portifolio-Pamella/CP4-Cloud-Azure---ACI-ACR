using Aegis.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aegis.Api.Repositories.Interfaces
{
    public interface IAlertaRepository
    {
        Task<IEnumerable<AlertaColisao>> GetAllAsync();
        Task<AlertaColisao?> GetByIdAsync(int id);
        Task AddAsync(AlertaColisao alerta);
        Task UpdateAsync(AlertaColisao alerta);
        Task DeleteAsync(int id);
    }
}