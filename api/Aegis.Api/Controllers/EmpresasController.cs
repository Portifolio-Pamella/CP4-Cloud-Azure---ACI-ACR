using Microsoft.AspNetCore.Mvc;
using Aegis.Api.Models;
using Aegis.Api.Repositories.Interfaces;

namespace Aegis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaRepository _repository;

        public EmpresasController(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            var empresas = await _repository.GetAllAsync();
            return Ok(empresas);
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _repository.GetByIdAsync(id);

            if (empresa == null)
            {
                return NotFound(new { mensagem = "Empresa não encontrada." });
            }

            return Ok(empresa);
        }

        // POST: api/Empresas
        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            await _repository.AddAsync(empresa);
            return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
        }

        // PUT: api/Empresas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest("ID da requisição não coincide com o ID do objeto.");
            }

            await _repository.UpdateAsync(empresa);
            return NoContent();
        }

        // DELETE: api/Empresas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}