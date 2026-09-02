using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;
using Aegis.Api.Services.Interfaces;

namespace Aegis.Api.Services
{
    public class SateliteService : ISateliteService
    {
        private readonly ISateliteRepository _repository;

        public SateliteService(ISateliteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Satelite>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Satelite?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Satelite satelite)
            => await _repository.AddAsync(satelite);

        public async Task UpdateAsync(Satelite satelite)
            => await _repository.UpdateAsync(satelite);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}