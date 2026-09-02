using Microsoft.AspNetCore.Mvc;
using Aegis.Api.Models;
using Aegis.Api.Services.Interfaces; // IMPORTANTE: Alterado para Services

namespace Aegis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissoesInterceptacaoController : ControllerBase
    {
        // Agora injetamos o Service, não o Repository
        private readonly IMissaoService _service;

        public MissoesInterceptacaoController(IMissaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissaoInterceptacao>>> GetMissoes()
        {
            // O Controller chama o Service
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MissaoInterceptacao>> GetMissao(int id)
        {
            var missao = await _service.GetByIdAsync(id);
            if (missao == null) return NotFound(new { mensagem = "Missão não encontrada." });
            return Ok(missao);
        }

        [HttpPost]
        public async Task<ActionResult<MissaoInterceptacao>> PostMissao(MissaoInterceptacao missao)
        {
            await _service.AddAsync(missao);
            return CreatedAtAction(nameof(GetMissao), new { id = missao.Id }, missao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMissao(int id, MissaoInterceptacao missao)
        {
            if (id != missao.Id) return BadRequest("ID inconsistente.");

            await _service.UpdateAsync(missao);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMissao(int id)
        {
            var missao = await _service.GetByIdAsync(id);
            if (missao == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}