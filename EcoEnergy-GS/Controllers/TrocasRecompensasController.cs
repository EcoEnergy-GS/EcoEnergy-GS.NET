using EcoEnergy_GS.DTO.HistoricoPontos;
using EcoEnergy_GS.DTO.TrocasRecompensas;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Services.HistoricoPontos;
using EcoEnergy_GS.Services.TrocasRecompensas;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy_GS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrocasRecompensasController : ControllerBase
    {
        private readonly ITrocasRecompensasInterface _trocasRecompensasInterface;

        public TrocasRecompensasController(ITrocasRecompensasInterface trocasRecompensasInterface)
        {
            _trocasRecompensasInterface = trocasRecompensasInterface;
        }

        [HttpGet("ListarTrocasRecompensas")]
        public async Task<ActionResult<ResponseModel<List<TrocasRecompensasModel>>>> ListarTrocasRecompensas()
        {
            var trocas = await _trocasRecompensasInterface.ListarTrocasRecompensas();
            return Ok(trocas);
        }

        [HttpGet("BucarTrocasRecompensasPorId/{id_trocas}")]
        public async Task<ActionResult<ResponseModel<TrocasRecompensasModel>>> BucarTrocasRecompensasPorId(int id_trocas)
        {
            var trocas = await _trocasRecompensasInterface.BucarTrocasRecompensasPorId(id_trocas);
            return Ok(trocas);
        }

        [HttpPost("CreateTrocasRecompensas")]
        public async Task<ActionResult<ResponseModel<TrocasRecompensasModel>>> CreateTrocasRecompensas(TrocasRecompensasCreateDto trocasRecompensasCreateDto)
        {
            var trocas = await _trocasRecompensasInterface.CreateTrocasRecompensas(trocasRecompensasCreateDto);
            return Ok(trocas);
        }

        [HttpPut("EditTrocasRecompensas/{id_trocas}")]
        public async Task<ActionResult<ResponseModel<TrocasRecompensasModel>>> EditTrocasRecompensas(int id_trocas, [FromBody] TrocasRecompensasEditDto trocasRecompensasEditDto)
        {
            if (id_trocas != trocasRecompensasEditDto.id_trocas)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var trocaRecompensa = await _trocasRecompensasInterface.EditTrocasRecompensas(trocasRecompensasEditDto);

            if (trocaRecompensa.Dados == null)
            {
                return NotFound("Troca de recompensa não encontrada!");
            }

            return NoContent();
        }

        [HttpDelete("DeleteTrocasRecompensas/{id_trocas}")]
        public async Task<ActionResult<ResponseModel<TrocasRecompensasModel>>> DeleteTrocasRecompensas(int id_trocas)
        {
            var trocaRecompensa = await _trocasRecompensasInterface.DeleteTrocasRecompensas(id_trocas);

            if (trocaRecompensa.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
