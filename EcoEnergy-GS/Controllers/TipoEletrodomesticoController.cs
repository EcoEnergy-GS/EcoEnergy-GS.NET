using EcoEnergy_GS.DTO.TipoEletrodomestico;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Services.TipoEletrodomestico;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy_GS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEletrodomesticoController : ControllerBase
    {
        private readonly ITipoEletrodomesticoInterface _tipoEletrodomesticoInterface;

        public TipoEletrodomesticoController(ITipoEletrodomesticoInterface tipoEletrodomesticoInterface)
        {
            _tipoEletrodomesticoInterface = tipoEletrodomesticoInterface;
        }

        [HttpGet("ListarTipoEletrodomestico")]
        [EndpointDescription("Lista o tipo de eletrodomestico")]
        public async Task<ActionResult<ResponseModel<List<TipoEletrodomesticoModel>>>> ListarTipoEletrodomestico()
        {
            var tipoEletrodomestico = await _tipoEletrodomesticoInterface.ListarTipoEletrodomestico();
            return Ok(tipoEletrodomestico);
        }

        [HttpGet("BucarTipoEletrodomesticoPorId/{id_eletrodomestico}")]
        [EndpointDescription("Lista o tipo de eletrodomestico de acordo com o ID.")]
        public async Task<ActionResult<ResponseModel<List<TipoEletrodomesticoModel>>>> BucarTipoEletrodomesticoPorId(int id_eletrodomestico)
        {
            var tipoEletrodomestico = await _tipoEletrodomesticoInterface.BucarTipoEletrodomesticoPorId(id_eletrodomestico);
            return Ok(tipoEletrodomestico);
        }

        [HttpPost("CreateTipoEletrodomestico")]
        [EndpointDescription("Cria um tipo de eletrodomestico")]
        public async Task<ActionResult<ResponseModel<TipoEletrodomesticoModel>>> CreateTipoEletrodomestico(TipoEletrodomesticoCreateDto tipoEletrodomesticoCreateDto)
        {
            var tipoEletrodomestico = await _tipoEletrodomesticoInterface.CreateTipoEletrodomestico(tipoEletrodomesticoCreateDto);
            return Ok(tipoEletrodomestico);
        }

        [HttpPut("EditTipoEletrodomestico/{id_eletrodomestico}")]
        [EndpointDescription("Edita um tipo de eletrodomestico de acordo com o ID")]
        public async Task<ActionResult<ResponseModel<TipoEletrodomesticoModel>>> EditTipoEletrodomestico(int id_eletrodomestico, [FromBody] TipoEletrodomesticoEditDto tipoEletrodomesticoEditDto)
        {
            if (id_eletrodomestico != tipoEletrodomesticoEditDto.id_eletrodomestico)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var tipoEletrodomestico = await _tipoEletrodomesticoInterface.EditTipoEletrodomestico(tipoEletrodomesticoEditDto);

            if (tipoEletrodomestico.Dados == null)
            {
                return NotFound("Tipo eletrodoméstico não encontrado");
            }

            return NoContent();
        }

        [HttpDelete("DeleteTipoEletrodomestico/{id_eletrodomestico}")]
        [EndpointDescription("Delete um tipo de eletrodomestico de acordo com o ID")]
        public async Task<ActionResult<ResponseModel<TipoEletrodomesticoModel>>> DeleteTipoEletrodomestico(int id_eletrodomestico)
        {
            var tipoEletrodomestico = await _tipoEletrodomesticoInterface.DeleteTipoEletrodomestico(id_eletrodomestico);

            if (tipoEletrodomestico.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
