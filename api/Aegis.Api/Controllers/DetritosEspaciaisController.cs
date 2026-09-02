using Microsoft.AspNetCore.Mvc;
using Aegis.Api.Models;
using Aegis.Api.Services.Interfaces; // Mude para Services

namespace Aegis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetritosEspaciaisController : ControllerBase
    {
        private readonly IDetritoService _service; // Injeta o Service

        public DetritosEspaciaisController(IDetritoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetritoEspacial>>> GetDetritos()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetritoEspacial>> GetDetrito(int id)
        {
            var detrito = await _service.GetByIdAsync(id);
            if (detrito == null) return NotFound(new { mensagem = "Detrito não encontrado." });

            return Ok(detrito);
        }

        [HttpPost]
        public async Task<ActionResult<DetritoEspacial>> PostDetrito(DetritoEspacial detrito)
        {
            await _service.AddAsync(detrito);
            return CreatedAtAction(nameof(GetDetrito), new { id = detrito.Id }, detrito);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetrito(int id, DetritoEspacial detrito)
        {
            if (id != detrito.Id) return BadRequest("ID da requisição não coincide.");

            await _service.UpdateAsync(detrito);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetrito(int id)
        {
            var detrito = await _service.GetByIdAsync(id);
            if (detrito == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}