using EcoEnergy_GS.DTO.HistoricoPontos;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Services.HistoricoPontos;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy_GS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoPontosController : ControllerBase
    {
        private readonly IHistoricoPontosInterface _historicoPontosInterface;

        public HistoricoPontosController(IHistoricoPontosInterface historicoPontosInterface)
        {
            _historicoPontosInterface = historicoPontosInterface;
        }

        [HttpGet("ListarHistorico")]
        public async Task<ActionResult<ResponseModel<List<HistoricoPontosModel>>>> ListarHistorico()
        {
            var historicos = await _historicoPontosInterface.ListarHistorico();
            return Ok(historicos);
        }

        [HttpGet("BucarHistoricoPorId/{id_historico}")]
        public async Task<ActionResult<ResponseModel<HistoricoPontosModel>>> BucarHistoricoPorId(int id_historico)
        {
            var historico = await _historicoPontosInterface.BucarHistoricoPorId(id_historico);
            return Ok(historico);
        }

        [HttpPost("CreateHistorico")]
        public async Task<ActionResult<ResponseModel<HistoricoPontosModel>>> CreateHistorico(HistoricoPontosCreateDto historicoPontosCreateDto)
        {
            var historico = await _historicoPontosInterface.CreateHistorico(historicoPontosCreateDto);
            return Ok(historico);
        }

        [HttpPut("EditHistorico/{id_historico}")]
        public async Task<ActionResult<ResponseModel<HistoricoPontosModel>>> EditHistorico(int id_historico, [FromBody] HistoricoPontosEditDto historicoPontosEditDto)
        {
            if (id_historico != historicoPontosEditDto.id_historico)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var historico = await _historicoPontosInterface.EditHistorico(historicoPontosEditDto);

            if (historico.Dados == null)
            {
                return NotFound("Histórico não encontrado");
            }

            return NoContent();
        }

        [HttpDelete("DeleteHistorico/{id_historico}")]
        public async Task<ActionResult<ResponseModel<HistoricoPontosModel>>> DeleteHistorico(int id_historico)
        {
            var historico = await _historicoPontosInterface.DeleteHistorico(id_historico);

            if (historico.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
