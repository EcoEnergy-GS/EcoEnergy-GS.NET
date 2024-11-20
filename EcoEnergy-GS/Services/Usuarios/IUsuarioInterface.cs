using EcoEnergy_GS.DTO.Usuarios;
using EcoEnergy_GS.Models;

namespace EcoEnergy_GS.Services.Usuarios
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioModel>>> ListarUsuarios();
        Task<ResponseModel<UsuarioModel>> BucarUsuarioPorId(int id_usuario);
        Task<ResponseModel<UsuarioModel>> CreateUsuario(UsuarioCreateDto usuarioCreateDto);
        Task<ResponseModel<UsuarioModel>> EditUsuario(UsuarioEditDto usuarioEditDto);
        Task<ResponseModel<UsuarioModel>> DeleteUsuario(int id_usuario);
    }
}
