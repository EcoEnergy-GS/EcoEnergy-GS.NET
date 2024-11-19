using EcoEnergy_GS.DTO.Usuarios;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Services.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy_GS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet("ListarUsuarios")]
        [EndpointDescription("Endpoint responsável por listar todos os usuários cadastrados.")]
        public async Task<ActionResult<ResponseModel<List<UsuarioModel>>>> ListarUsuarios()
        {
            var usuarios = await _usuarioInterface.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("BucarUsuarioPorId/{id_usuario}")]
        [EndpointDescription("Endpoint responsável por buscar usuário de acordo com seu id.")]
        public async Task<ActionResult<ResponseModel<List<UsuarioModel>>>> BucarUsuarioPorId(int id_usuario)
        {
            var usuario = await _usuarioInterface.BucarUsuarioPorId(id_usuario);
            return Ok(usuario);
        }

        [HttpPost("CreateUsuario")]
        [EndpointDescription("Endpoint responsável por criar um usuário novo.")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> CreateUsuario(UsuarioCreateDto usuarioCreateDto)
        {
            var usuario = await _usuarioInterface.CreateUsuario(usuarioCreateDto);
            return Ok(usuario);
        }

        [HttpPut("EditUsuario/{id_user}")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> EditUsuario(int id_user, [FromBody] UsuarioEditDto usuarioEditDto)
        {
            if(id_user != usuarioEditDto.id_usuarios)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var user =  await _usuarioInterface.EditUsuario(usuarioEditDto);

            if (user.Dados == null)
            {
                return NotFound("Usuário não encontrado");
            }
            
            return NoContent();
        }

        [HttpDelete("DeleteUsuario/{id_user}")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> DeleteUsuario(int id_user)
        {
            var user = await _usuarioInterface.DeleteUsuario(id_user);

            if (user.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
