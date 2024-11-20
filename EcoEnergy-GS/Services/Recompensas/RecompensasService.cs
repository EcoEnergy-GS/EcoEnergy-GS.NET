using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.Recompensas;
using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy_GS.Services.Recompensas
{
    public class RecompensasService : IRecompensasInterface
    {
        public readonly AppDbContext _context;

        public RecompensasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<RecompensasModel>> BucarRecompensasPorId(int id_recompensas)
        {
            ResponseModel<RecompensasModel> resposta = new ResponseModel<RecompensasModel>();

            try
            {
                var recompensas = await _context.Recompensas.FirstOrDefaultAsync(recompensasBanco => recompensasBanco.id_recompensas == id_recompensas);

                if (recompensas == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado!";
                    return resposta;
                }

                resposta.Dados = recompensas;
                resposta.Mensagem = "Recompensa localizada!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<RecompensasModel>>> ListarRecompensas()
        {
            ResponseModel<List<RecompensasModel>> resposta = new ResponseModel<List<RecompensasModel>>();

            try
            {
                var recompensas = await _context.Recompensas.ToListAsync();

                resposta.Dados = recompensas;
                resposta.Mensagem = "Todas as recompensas coletadas!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<RecompensasModel>> CreateRecompensas(RecompensasCreateDto recompensasCreateDto)
        {
            ResponseModel<RecompensasModel> resposta = new ResponseModel<RecompensasModel>();

            try
            {
                var recompensas = new RecompensasModel()
                {
                    descricao = recompensasCreateDto.descricao,
                    pontos_necessarios = recompensasCreateDto.pontos_necessarios
                };

                _context.Add(recompensas);
                await _context.SaveChangesAsync();

                resposta.Dados = recompensas;
                resposta.Mensagem = "Recompensa criada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<RecompensasModel>> DeleteRecompensas(int id_recompensas)
        {
            ResponseModel<RecompensasModel> resposta = new ResponseModel<RecompensasModel>();

            try
            {
                var recompensas = await _context.Recompensas.FirstOrDefaultAsync(recompensasBanco => recompensasBanco.id_recompensas == id_recompensas);

                if (recompensas == null)
                {
                    resposta.Mensagem = "Nenhuma recompensa localizada!";
                    return resposta;
                }

                _context.Remove(recompensas);
                await _context.SaveChangesAsync();

                resposta.Dados = recompensas;
                resposta.Mensagem = "Recompensa deletada com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<RecompensasModel>> EditRecompensas(RecompensasEditDto recompensasEditDto)
        {
            ResponseModel<RecompensasModel> resposta = new ResponseModel<RecompensasModel>();

            try
            {
                var recompensas = await _context.Recompensas
                    .FirstOrDefaultAsync(
                    recompensasBanco => recompensasBanco.id_recompensas == recompensasEditDto.id_recompensas);

                //var recompensas = await _context.Recompensas.FindAsync(recompensasEditDto.id_recompensas);

                if (recompensas == null)
                {
                    resposta.Mensagem = "Nenhuma recompensa localizada!";
                    return resposta;
                }

                recompensas.descricao = recompensasEditDto.descricao;
                recompensas.pontos_necessarios = recompensasEditDto.pontos_necessarios;

                _context.Update(recompensas);
                await _context.SaveChangesAsync();

                resposta.Dados = recompensas;
                resposta.Mensagem = "Recompensa editada com sucesso!";
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
