using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;
using Aegis.Api.Services.Interfaces;

namespace Aegis.Api.Services
{
    public class DetritoService : IDetritoService
    {
        private readonly IDetritoRepository _repository;
        public DetritoService(IDetritoRepository repository) => _repository = repository;

        public async Task<IEnumerable<DetritoEspacial>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<DetritoEspacial?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(DetritoEspacial detrito) => await _repository.AddAsync(detrito);
        public async Task UpdateAsync(DetritoEspacial detrito) => await _repository.UpdateAsync(detrito);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}