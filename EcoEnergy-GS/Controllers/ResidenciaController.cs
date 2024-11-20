using EcoEnergy_GS.DTO.Recompensas;
using EcoEnergy_GS.DTO.Residencia;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Services.Recompensas;
using EcoEnergy_GS.Services.Residencia;
using EcoEnergy_GS.Services.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy_GS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciaController : ControllerBase
    {
        private readonly IResidenciaInterface _residenciaInterface;

        public ResidenciaController(IResidenciaInterface residenciaInterface)
        {
            _residenciaInterface = residenciaInterface;
        }

        [HttpGet("ListarResidencia")]
        public async Task<ActionResult<ResponseModel<List<ResidenciaModel>>>> ListarResidencia()
        {
            var residencia = await _residenciaInterface.ListarResidencia();
            return Ok(residencia);
        }

        [HttpGet("BucarResidenciaPorId/{id_residencia}")]
        public async Task<ActionResult<ResponseModel<List<ResidenciaModel>>>> BucarResidenciaPorId(int id_residencia)
        {
            var residencia = await _residenciaInterface.BucarResidenciaPorId(id_residencia);
            return Ok(residencia);
        }

        [HttpPost("CreateResidencia")]
        public async Task<ActionResult<ResponseModel<ResidenciaModel>>> CreateResidencia(ResidenciaCreateDto residenciaCreateDto)
        {
            var residencia = await _residenciaInterface.CreateResidencia(residenciaCreateDto);
            return Ok(residencia);
        }

        [HttpPut("EditResidencia/{id_residencia}")]
        public async Task<ActionResult<ResponseModel<ResidenciaModel>>> EditResidencia(int id_residencia, [FromBody] ResidenciaEditDto residenciaEditDto)
        {
            if (id_residencia != residenciaEditDto.id_residencia)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var residencia = await _residenciaInterface.EditResidencia(residenciaEditDto);

            if (residencia.Dados == null)
            {
                return NotFound("Recompensa não encontrada");
            }

            return NoContent();
        }

        [HttpDelete("DeleteResidencia/{id_residencia}")]
        public async Task<ActionResult<ResponseModel<ResidenciaModel>>> DeleteResidencia(int id_residencia)
        {
            var residencia = await _residenciaInterface.DeleteResidencia(id_residencia);

            if (residencia.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
