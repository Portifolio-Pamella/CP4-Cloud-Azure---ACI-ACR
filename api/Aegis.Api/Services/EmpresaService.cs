using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;
using Aegis.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aegis.Api.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Empresa?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Empresa empresa)
            => await _repository.AddAsync(empresa);

        public async Task UpdateAsync(Empresa empresa)
            => await _repository.UpdateAsync(empresa);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}