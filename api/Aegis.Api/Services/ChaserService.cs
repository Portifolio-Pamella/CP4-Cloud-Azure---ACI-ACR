using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;
using Aegis.Api.Services.Interfaces;

namespace Aegis.Api.Services
{
    public class ChaserService : IChaserService
    {
        private readonly IChaserRepository _repository;
        public ChaserService(IChaserRepository repository) => _repository = repository;

        public async Task<IEnumerable<Chaser>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Chaser?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(Chaser chaser) => await _repository.AddAsync(chaser);
        public async Task UpdateAsync(Chaser chaser) => await _repository.UpdateAsync(chaser);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}