using EcoEnergy_GS.DTO.ConsumoEnergia;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Services.ConsumoEnergia;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcoEnergy_GS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoEnergiaController : ControllerBase
    {
        private readonly IConsumoEnergiaInterface _consumoEnergiaInterface;

        public ConsumoEnergiaController(IConsumoEnergiaInterface consumoEnergiaInterface)
        {
            _consumoEnergiaInterface = consumoEnergiaInterface;
        }

        [HttpGet("ListarConsumoEnergia")]
        [EndpointDescription("Lista o consumo de energia")]
        public async Task<ActionResult<ResponseModel<List<ConsumoEnergiaModel>>>> ListarConsumoEnergia()
        {
            var consumo = await _consumoEnergiaInterface.ListarConsumoEnergia();
            return Ok(consumo);
        }

        [HttpGet("BucarConsumoEnergiaPorId/{id_consumo}")]
        [EndpointDescription("Lista o consumo de energia de acordo com o ID.")]
        public async Task<ActionResult<ResponseModel<List<ConsumoEnergiaModel>>>> BucarConsumoEnergiaPorId(int id_consumo)
        {
            var consumo = await _consumoEnergiaInterface.BucarConsumoEnergiaPorId(id_consumo);
            return Ok(consumo);
        }

        [HttpPost("CreateConsumoEnergia")]
        [EndpointDescription("Cria um novo consumo de energia")]
        public async Task<ActionResult<ResponseModel<ConsumoEnergiaModel>>> CreateConsumoEnergia(ConsumoEnergiaCreateDto consumoEnergiaCreateDto)
        {
            var consumo = await _consumoEnergiaInterface.CreateConsumoEnergia(consumoEnergiaCreateDto);
            return Ok(consumo);
        }

        [HttpPut("EditConsumoEnergia/{id_consumo}")]
        [EndpointDescription("Edita um consumo de energia de acordo com o ID")]
        public async Task<ActionResult<ResponseModel<ConsumoEnergiaModel>>> EditConsumoEnergia(int id_consumo, [FromBody] ConsumoEnergiaEditDto consumoEnergiaEditDto)
        {
            if (id_consumo != consumoEnergiaEditDto.id_consumo)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var consumo = await _consumoEnergiaInterface.EditConsumoEnergia(consumoEnergiaEditDto);

            if (consumo.Dados == null)
            {
                return NotFound("Recompensa não encontrada");
            }

            return NoContent();
        }

        [HttpDelete("DeleteConsumoEnergia/{id_consumo}")]
        [EndpointDescription("Delete um consumo de energia de acordo com o ID")]
        public async Task<ActionResult<ResponseModel<ConsumoEnergiaModel>>> DeleteConsumoEnergia(int id_consumo)
        {
            var consumo = await _consumoEnergiaInterface.DeleteConsumoEnergia(id_consumo);

            if (consumo.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
