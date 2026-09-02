using Microsoft.AspNetCore.Mvc;
using Aegis.Api.Models;
using Aegis.Api.Services.Interfaces; // Agora injetamos o Service

namespace Aegis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SatelitesController : ControllerBase
    {
        private readonly ISateliteService _service;

        public SatelitesController(ISateliteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Satelite>>> GetSatelites()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Satelite>> GetSatelite(int id)
        {
            var satelite = await _service.GetByIdAsync(id);
            if (satelite == null) return NotFound(new { mensagem = "Satélite não encontrado." });

            return Ok(satelite);
        }

        [HttpPost]
        public async Task<ActionResult<Satelite>> PostSatelite(Satelite satelite)
        {
            await _service.AddAsync(satelite);
            return CreatedAtAction(nameof(GetSatelite), new { id = satelite.Id }, satelite);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSatelite(int id, Satelite satelite)
        {
            if (id != satelite.Id) return BadRequest("ID inconsistente.");

            try
            {
                await _service.UpdateAsync(satelite);
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSatelite(int id)
        {
            var satelite = await _service.GetByIdAsync(id);
            if (satelite == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}