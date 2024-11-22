using EcoEnergy_GS.DTO.Recompensas;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Services.Recompensas;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy_GS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecompensasController : ControllerBase
    {
        private readonly IRecompensasInterface _recompensasInterface;

        public RecompensasController(IRecompensasInterface recompensasInterface)
        {
            _recompensasInterface = recompensasInterface;
        }

        [HttpGet("ListarRecompensas")]
        [EndpointDescription("Lista as recompensas")]
        public async Task<ActionResult<ResponseModel<List<RecompensasModel>>>> ListarRecompensas()
        {
            var recompensas = await _recompensasInterface.ListarRecompensas();
            return Ok(recompensas);
        }

        [HttpGet("BucarRecompensasPorId/{id_recompensas}")]
        [EndpointDescription("Lista as recompensas de acordo com o ID.")]
        public async Task<ActionResult<ResponseModel<List<RecompensasModel>>>> BucarRecompensasPorId(int id_recompensas)
        {
            var recompensas = await _recompensasInterface.BucarRecompensasPorId(id_recompensas);
            return Ok(recompensas);
        }

        [HttpPost("CreateRecompensas")]
        [EndpointDescription("Cria uma recompensa")]
        public async Task<ActionResult<ResponseModel<RecompensasModel>>> CreateRecompensas(RecompensasCreateDto recompensasCreateDto)
        {
            var recompensas = await _recompensasInterface.CreateRecompensas(recompensasCreateDto);
            return Ok(recompensas);
        }

        [HttpPut("EditRecompensas/{id_recompensas}")]
        [EndpointDescription("Edita uma recompensa de acordo com o ID")]
        public async Task<ActionResult<ResponseModel<RecompensasModel>>> EditRecompensas(int id_recompensas, [FromBody] RecompensasEditDto recompensasEditDto)
        {
            if (id_recompensas != recompensasEditDto.id_recompensas)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var recompensas = await _recompensasInterface.EditRecompensas(recompensasEditDto);

            if (recompensas.Dados == null)
            {
                return NotFound("Recompensa não encontrada");
            }

            return NoContent();
        }

        [HttpDelete("DeleteRecompensas/{id_recompensas}")]
        [EndpointDescription("Delete uma recompensa de acordo com o ID")]
        public async Task<ActionResult<ResponseModel<RecompensasModel>>> DeleteRecompensas(int id_recompensas)
        {
            var recompensas = await _recompensasInterface.DeleteRecompensas(id_recompensas);

            if (recompensas.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
