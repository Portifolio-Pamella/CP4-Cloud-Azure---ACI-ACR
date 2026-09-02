using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;
using Aegis.Api.Services.Interfaces;

namespace Aegis.Api.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _repository;
        public AlertaService(IAlertaRepository repository) => _repository = repository;

        public async Task<IEnumerable<AlertaColisao>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<AlertaColisao?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(AlertaColisao alerta) => await _repository.AddAsync(alerta);
        public async Task UpdateAsync(AlertaColisao alerta) => await _repository.UpdateAsync(alerta);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}