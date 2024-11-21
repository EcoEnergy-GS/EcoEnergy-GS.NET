using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.HistoricoPontos;
using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy_GS.Services.HistoricoPontos
{
    public class HistoricoPontosService : IHistoricoPontosInterface
    {
        public readonly AppDbContext _context;

        public HistoricoPontosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<HistoricoPontosModel>>> ListarHistorico()
        {
            ResponseModel<List<HistoricoPontosModel>> resposta = new ResponseModel<List<HistoricoPontosModel>>();

            try
            {
                var historico = await _context.HistoricoPontos.Include(u => u.Usuario).ToListAsync();

                resposta.Dados = historico;
                resposta.Mensagem = "Todos históricos de pontos coletados!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<ResponseModel<HistoricoPontosModel>> BucarHistoricoPorId(int id_historico)
        {
            ResponseModel<HistoricoPontosModel> resposta = new ResponseModel<HistoricoPontosModel>();

            try
            {
                var historico = await _context.HistoricoPontos.Include(u => u.Usuario).FirstOrDefaultAsync(historicoDb => historicoDb.id_historico == id_historico);

                if (historico == null)
                {
                    resposta.Mensagem = "Nenhum histórico localizado!";
                    return resposta;
                }

                resposta.Dados = historico;
                resposta.Mensagem = "Histórico localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<HistoricoPontosModel>> CreateHistorico(HistoricoPontosCreateDto historicoCreateDto)
        {
            ResponseModel<HistoricoPontosModel> resposta = new ResponseModel<HistoricoPontosModel>();
            try
            {
                var historicoDb = await _context.HistoricoPontos
                    .Include(u => u.Usuario)
                    .FirstOrDefaultAsync(
                        u =>
                        u.Usuario.id_usuarios == historicoCreateDto.id_usuarios
                    );

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioDb => usuarioDb.id_usuarios == historicoCreateDto.id_usuarios);

                if (usuario == null)
                {
                    resposta.Mensagem = "Usuário não encontrado!";
                    return resposta;
                }

                var historico = new HistoricoPontosModel()
                {
                    id_usuarios = historicoCreateDto.id_usuarios,
                    data_historico = historicoCreateDto.data_historico,
                    quantidade = historicoCreateDto.quantidade,
                    Usuario = usuario
                };

                _context.Add(historico);
                await _context.SaveChangesAsync();

                resposta.Dados = historico;
                resposta.Mensagem = "Histórico de pontos criado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Ocorreu um erro ao criar a Historico de pontos: " + ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<HistoricoPontosModel>> DeleteHistorico(int id_historico)
        {
            ResponseModel<HistoricoPontosModel> resposta = new ResponseModel<HistoricoPontosModel>();
            try
            {
                var historico = await _context.HistoricoPontos
                    .FirstOrDefaultAsync(historicoDb => historicoDb.id_historico == id_historico);

                if (historico == null)
                {
                    resposta.Mensagem = "Nenhum histórico encontrado!";
                    return resposta;
                }

                _context.Remove(historico);
                await _context.SaveChangesAsync();

                resposta.Dados = historico;
                resposta.Mensagem = "Histórico removido com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<HistoricoPontosModel>> EditHistorico(HistoricoPontosEditDto historicoEditDto)
        {
            ResponseModel<HistoricoPontosModel> resposta = new ResponseModel<HistoricoPontosModel>();
            try
            {
                var historico = await _context.HistoricoPontos
                    .Include(u => u.Usuario)
                    .FirstOrDefaultAsync(
                        historicoDb => historicoDb.id_historico == historicoEditDto.id_historico
                    );
                if (historico == null)
                {
                    resposta.Mensagem = "Nenhum registro de histórico localizado!";
                    return resposta;
                }

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(usuarioDb => usuarioDb.id_usuarios == historicoEditDto.id_usuarios);

                if (usuario == null)
                {
                    resposta.Mensagem = "Nenhum registro de usuário localizado!";
                    return resposta;
                }

                historico.data_historico = historicoEditDto.data_historico;
                historico.quantidade = historicoEditDto.quantidade;
                historico.Usuario = usuario;

                resposta.Mensagem = "Histórico editado com sucesso!";
                resposta.Status = true;

                _context.Update(historico);
                await _context.SaveChangesAsync();

                resposta.Dados = historico;
                return resposta;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
