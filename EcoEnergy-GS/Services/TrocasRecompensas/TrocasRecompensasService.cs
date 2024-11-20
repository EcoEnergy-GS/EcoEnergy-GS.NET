using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.TrocasRecompensas;
using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy_GS.Services.TrocasRecompensas
{
    public class TrocasRecompensasService : ITrocasRecompensasInterface
    {
        public readonly AppDbContext _context;

        public TrocasRecompensasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<TrocasRecompensasModel>>> ListarTrocasRecompensas()
        {
            ResponseModel<List<TrocasRecompensasModel>> resposta = new ResponseModel<List<TrocasRecompensasModel>>();

            try
            {
                var trocasRecompensas = await _context.TrocasRecompensas.Include(u => u.Usuario).Include(r => r.Recompensas).ToListAsync();

                resposta.Dados = trocasRecompensas;
                resposta.Mensagem = "Todas trocas de recompensas coletadas!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<TrocasRecompensasModel>> BucarTrocasRecompensasPorId(int id_trocas)
        {
            ResponseModel<TrocasRecompensasModel> resposta = new ResponseModel<TrocasRecompensasModel>();

            try
            {
                var trocasRecompensas = await _context.TrocasRecompensas.Include(u => u.Usuario).Include(r => r.Recompensas).FirstOrDefaultAsync(trocasDb => trocasDb.id_trocas == id_trocas);

                if (trocasRecompensas == null)
                {
                    resposta.Mensagem = "Nenhuma troca de recompensa localizada!";
                    return resposta;
                }

                resposta.Dados = trocasRecompensas;
                resposta.Mensagem = "Troca de recompensa localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<TrocasRecompensasModel>> CreateTrocasRecompensas(TrocasRecompensasCreateDto trocasRecompensasCreateDto)
        {
            ResponseModel<TrocasRecompensasModel> resposta = new ResponseModel<TrocasRecompensasModel>();
            try
            {
                var trocasDb = await _context.TrocasRecompensas
                    .Include(u => u.Usuario)
                    .Include(r => r.Recompensas)
                    .FirstOrDefaultAsync(
                        t =>
                        t.Usuario.id_usuarios == trocasRecompensasCreateDto.id_usuarios &&
                        t.Recompensas.id_recompensas == trocasRecompensasCreateDto.id_recompensas
                    );

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioDb => usuarioDb.id_usuarios == trocasRecompensasCreateDto.id_usuarios);

                if (usuario == null)
                {
                    resposta.Mensagem = "Usuário não encontrado!";
                    return resposta;
                }

                var recompensas = await _context.Recompensas.FirstOrDefaultAsync(recompensasDb => recompensasDb.id_recompensas == trocasRecompensasCreateDto.id_recompensas);

                if (recompensas == null)
                {
                    resposta.Mensagem = "Recompensa não encontrada!";
                    return resposta;
                }

                var trocasRecompensas = new TrocasRecompensasModel()
                {
                    data_troca = trocasRecompensasCreateDto.data_troca,
                    pontos_utilizados = trocasRecompensasCreateDto.pontos_utilizados,
                    Recompensas = recompensas,
                    Usuario = usuario
                };

                _context.Add(trocasRecompensas);
                _context.SaveChanges();

                resposta.Dados = trocasRecompensas;
                resposta.Mensagem = "Troca recompensa criada com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Ocorreu um erro ao criar a Troca de recompensa: " + ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<TrocasRecompensasModel>> DeleteTrocasRecompensas(int id_trocas)
        {
            ResponseModel<TrocasRecompensasModel> resposta = new ResponseModel<TrocasRecompensasModel>();

            try
            {
                var trocasRecompensas = await _context.TrocasRecompensas
                    .FirstOrDefaultAsync(trocasDb => trocasDb.id_trocas == id_trocas);

                if (trocasRecompensas == null)
                {
                    resposta.Mensagem = "Nenhuma troca de recompensa encontrada!";
                }

                _context.Remove(trocasRecompensas);
                await _context.SaveChangesAsync();

                resposta.Dados = trocasRecompensas;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<TrocasRecompensasModel>> EditTrocasRecompensas(TrocasRecompensasEditDto trocasRecompensasEditDto)
        {
            ResponseModel<TrocasRecompensasModel> resposta = new ResponseModel<TrocasRecompensasModel>();

            try
            {
                var trocas = await _context.TrocasRecompensas
                    .Include(u => u.Usuario)
                    .Include(r => r.Recompensas)
                    .FirstOrDefaultAsync(
                        trocasDb => trocasDb.id_trocas == trocasRecompensasEditDto.id_trocas
                    );

                if ( trocas == null)
                {
                    resposta.Mensagem = "Nenhum registro de trocas recompensas encontrado!";
                    return resposta;
                }

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(usuarioDb => usuarioDb.id_usuarios == trocasRecompensasEditDto.id_usuarios);

                if ( usuario == null)
                {
                    resposta.Mensagem = "Nenhum registro de usuario encontrado!";
                    return resposta;
                }

                var recompensas = await _context.Recompensas
                    .FirstOrDefaultAsync(recompensasDb => recompensasDb.id_recompensas == trocasRecompensasEditDto.id_recompensas);

                if ( recompensas == null)
                {
                    resposta.Mensagem = "Nenhum registro de recompensas encontrado!";
                    return resposta;
                }

                var trocasrecompensas = new TrocasRecompensasModel()
                {
                    id_recompensas = trocasRecompensasEditDto.id_recompensas,
                    id_usuarios = trocasRecompensasEditDto.id_usuarios,
                    data_troca = trocasRecompensasEditDto.data_troca,
                    pontos_utilizados = trocasRecompensasEditDto.pontos_utilizados,
                    Recompensas = recompensas,
                    Usuario = usuario
                };

                _context.Add(trocasrecompensas);
                await _context.SaveChangesAsync();

                resposta.Dados = trocasrecompensas;
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
