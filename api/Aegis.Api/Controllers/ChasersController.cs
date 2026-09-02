using Microsoft.AspNetCore.Mvc;
using Aegis.Api.Models;
using Aegis.Api.Services.Interfaces; // Mude para Services

namespace Aegis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChasersController : ControllerBase
    {
        private readonly IChaserService _service; // Injeta o Service

        public ChasersController(IChaserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chaser>>> GetChasers()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chaser>> GetChaser(int id)
        {
            var chaser = await _service.GetByIdAsync(id);
            if (chaser == null) return NotFound(new { mensagem = "Chaser não encontrado." });

            return Ok(chaser);
        }

        [HttpPost]
        public async Task<ActionResult<Chaser>> PostChaser(Chaser chaser)
        {
            await _service.AddAsync(chaser);
            return CreatedAtAction(nameof(GetChaser), new { id = chaser.Id }, chaser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChaser(int id, Chaser chaser)
        {
            if (id != chaser.Id) return BadRequest("ID da requisição não coincide.");

            await _service.UpdateAsync(chaser);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChaser(int id)
        {
            var chaser = await _service.GetByIdAsync(id);
            if (chaser == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}