using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.Usuarios;
using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy_GS.Services.Usuarios
{
    public class UsuarioService : IUsuarioInterface
    {
        public readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<UsuarioModel>> BucarUsuarioPorId(int id_usuario)
        {
            ResponseModel<UsuarioModel> resposta = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.id_usuarios == id_usuario);

                if (usuario == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado!";
                    return resposta;
                }

                resposta.Dados = usuario;
                resposta.Mensagem = "Usuário localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<UsuarioModel>>> ListarUsuarios()
        {
            ResponseModel<List<UsuarioModel>> resposta = new ResponseModel<List<UsuarioModel>>();

            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();

                resposta.Dados = usuarios;
                resposta.Mensagem = "Todos os usuários coletados!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> CreateUsuario(UsuarioCreateDto usuarioCreateDto)
        {
            ResponseModel<UsuarioModel> resposta = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = new UsuarioModel()
                {
                    nome = usuarioCreateDto.nome,
                    senha = usuarioCreateDto.senha,
                    telefone = usuarioCreateDto.telefone,
                    pontos = usuarioCreateDto.pontos
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                resposta.Dados = usuario;
                resposta.Mensagem = "Usuário criado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> DeleteUsuario(int id_usuario)
        {
            ResponseModel<UsuarioModel> resposta = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.id_usuarios == id_usuario);

                if (usuario == null)
                {
                    resposta.Mensagem = "Nenhum usuário localizado!";
                    return resposta;
                }

                _context.Remove(usuario);
                await _context.SaveChangesAsync();

                resposta.Dados = usuario;
                resposta.Mensagem = "Usuário deletado com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> EditUsuario(UsuarioEditDto usuarioEditDto)
        {
            ResponseModel<UsuarioModel> resposta = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(
                    usuarioBanco => usuarioBanco.id_usuarios == usuarioEditDto.id_usuarios);

                if (usuario == null)
                {
                    resposta.Mensagem = "Nenhum usuário localizado!";
                    return resposta;
                }

                usuario.nome = usuarioEditDto.nome;
                usuario.senha = usuarioEditDto.senha;
                usuario.telefone = usuarioEditDto.telefone;
                usuario.pontos = usuarioEditDto.pontos;

                _context.Update(usuario);
                await _context.SaveChangesAsync();

                resposta.Dados = usuario;
                resposta.Mensagem = "Usuário editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
