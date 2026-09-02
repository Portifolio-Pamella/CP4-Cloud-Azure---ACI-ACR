using Microsoft.AspNetCore.Mvc;
using Aegis.Api.Models;
using Aegis.Api.Services.Interfaces; // Mude para Services

namespace Aegis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasColisaoController : ControllerBase
    {
        private readonly IAlertaService _service; // Injeta o Service

        public AlertasColisaoController(IAlertaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaColisao>>> GetAlertas()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaColisao>> GetAlerta(int id)
        {
            var alerta = await _service.GetByIdAsync(id);
            if (alerta == null) return NotFound(new { mensagem = "Alerta de colisão não encontrado." });

            return Ok(alerta);
        }

        [HttpPost]
        public async Task<ActionResult<AlertaColisao>> PostAlerta(AlertaColisao alerta)
        {
            await _service.AddAsync(alerta);
            return CreatedAtAction(nameof(GetAlerta), new { id = alerta.Id }, alerta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlerta(int id, AlertaColisao alerta)
        {
            if (id != alerta.Id) return BadRequest("ID da requisição não coincide.");

            await _service.UpdateAsync(alerta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlerta(int id)
        {
            var alerta = await _service.GetByIdAsync(id);
            if (alerta == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}