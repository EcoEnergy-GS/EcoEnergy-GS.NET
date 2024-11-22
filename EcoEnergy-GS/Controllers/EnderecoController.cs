using EcoEnergy_GS.DTO.Endereco;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Services.Endereco;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy_GS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoInterface _enderecoInterface;

        public EnderecoController(IEnderecoInterface enderecoInterface)
        {
            _enderecoInterface = enderecoInterface;
        }


        [HttpGet("ListarEndereco")]
        [EndpointDescription("Lista o endereço")]
        public async Task<ActionResult<ResponseModel<List<EnderecoModel>>>> ListarEndereco()
        {
            var endereco = await _enderecoInterface.ListarEndereco();
            return Ok(endereco);
        }

        [HttpGet("BucarEnderecoPorId/{id_endereco}")]
        [EndpointDescription("Lista o endereço de acordo com o ID.")]
        public async Task<ActionResult<ResponseModel<List<EnderecoModel>>>> BucarEnderecoPorId(int id_endereco)
        {
            var endereco = await _enderecoInterface.BucarEnderecoPorId(id_endereco);
            return Ok(endereco);
        }

        [HttpPost("CreateEndereco")]
        [EndpointDescription("Cria um endereço")]
        public async Task<ActionResult<ResponseModel<EnderecoModel>>> CreateEndereco(EnderecoCreateDto enderecoCreateDto)
        {
            var endereco = await _enderecoInterface.CreateEndereco(enderecoCreateDto);
            return Ok(endereco);
        }

        [HttpPut("EditEndereco/{id_endereco}")]
        [EndpointDescription("Edita um endereço de acordo com o ID")]
        public async Task<ActionResult<ResponseModel<EnderecoModel>>> EditEndereco(int id_endereco, [FromBody] EnderecoEditDto enderecoEditDto)
        {
            if (id_endereco != enderecoEditDto.id_endereco)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var endereco = await _enderecoInterface.EditEndereco(enderecoEditDto);

            if (endereco.Dados == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return NoContent();
        }

        [HttpDelete("DeleteEndereco/{id_endereco}")]
        [EndpointDescription("Delete um endereço de acordo com o ID")]
        public async Task<ActionResult<ResponseModel<EnderecoModel>>> DeleteEndereco(int id_endereco)
        {
            var endereco = await _enderecoInterface.DeleteEndereco(id_endereco);

            if (endereco.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
