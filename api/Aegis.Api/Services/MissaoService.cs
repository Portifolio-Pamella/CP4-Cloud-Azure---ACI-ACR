using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;
using Aegis.Api.Services.Interfaces;

namespace Aegis.Api.Services
{
    public class MissaoService : IMissaoService
    {
        private readonly IMissaoRepository _repository;

        public MissaoService(IMissaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MissaoInterceptacao>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<MissaoInterceptacao?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(MissaoInterceptacao missao)
            => await _repository.AddAsync(missao);

        public async Task UpdateAsync(MissaoInterceptacao missao)
            => await _repository.UpdateAsync(missao);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}