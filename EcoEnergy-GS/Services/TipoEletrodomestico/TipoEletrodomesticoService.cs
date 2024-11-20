using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.TipoEletrodomestico;
using EcoEnergy_GS.DTO.Usuarios;
using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy_GS.Services.TipoEletrodomestico
{
    public class TipoEletrodomesticoService : ITipoEletrodomesticoInterface
    {
        public readonly AppDbContext _context;

        public TipoEletrodomesticoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<TipoEletrodomesticoModel>> BucarTipoEletrodomesticoPorId(int id_eletrodomestico)
        {
            ResponseModel<TipoEletrodomesticoModel> resposta = new ResponseModel<TipoEletrodomesticoModel>();

            try
            {
                var tipoEletrodomestico = await _context.TipoEletrodomestico.FirstOrDefaultAsync(tipoBanco => tipoBanco.id_eletrodomestico == id_eletrodomestico);

                if (tipoEletrodomestico == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado!";
                    return resposta;
                }

                resposta.Dados = tipoEletrodomestico;
                resposta.Mensagem = "Tipo eletrodoméstico localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<TipoEletrodomesticoModel>>> ListarTipoEletrodomestico()
        {
            ResponseModel<List<TipoEletrodomesticoModel>> resposta = new ResponseModel<List<TipoEletrodomesticoModel>>();

            try
            {
                var tipoEletrodomestico = await _context.TipoEletrodomestico.ToListAsync();

                resposta.Dados = tipoEletrodomestico;
                resposta.Mensagem = "Todos os tipos eletrodoméstico coletados!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<TipoEletrodomesticoModel>> CreateTipoEletrodomestico(TipoEletrodomesticoCreateDto tipoEletrodomesticoCreateDto)
        {
            ResponseModel<TipoEletrodomesticoModel> resposta = new ResponseModel<TipoEletrodomesticoModel>();

            try
            {
                var tipoEletrodomestico = new TipoEletrodomesticoModel()
                {
                    nome_eletrodomestico = tipoEletrodomesticoCreateDto.nome_eletrodomestico,
                    quantidade = tipoEletrodomesticoCreateDto.quantidade
                };

                _context.Add(tipoEletrodomestico);
                await _context.SaveChangesAsync();

                resposta.Dados = tipoEletrodomestico;
                resposta.Mensagem = "Tipo eletrodoméstico criado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<TipoEletrodomesticoModel>> DeleteTipoEletrodomestico(int id_eletrodomestico)
        {
            ResponseModel<TipoEletrodomesticoModel> resposta = new ResponseModel<TipoEletrodomesticoModel>();

            try
            {
                var tipoEletrodomestico = await _context.TipoEletrodomestico.FirstOrDefaultAsync(tipoBanco => tipoBanco.id_eletrodomestico == id_eletrodomestico);

                if (tipoEletrodomestico == null)
                {
                    resposta.Mensagem = "Nenhum tipo eletrodoméstico localizado!";
                    return resposta;
                }

                _context.Remove(tipoEletrodomestico);
                await _context.SaveChangesAsync();

                resposta.Dados = tipoEletrodomestico;
                resposta.Mensagem = "Tipo eletrodoméstico deletado com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<TipoEletrodomesticoModel>> EditTipoEletrodomestico(TipoEletrodomesticoEditDto tipoEletrodomesticoEditDto)
        {
            ResponseModel<TipoEletrodomesticoModel> resposta = new ResponseModel<TipoEletrodomesticoModel>();

            try
            {
                var tipoEletrodomestico = await _context.TipoEletrodomestico
                    .FirstOrDefaultAsync(
                    tipoBanco => tipoBanco.id_eletrodomestico == tipoEletrodomesticoEditDto.id_eletrodomestico);

                if (tipoEletrodomestico == null)
                {
                    resposta.Mensagem = "Nenhum tipo eletrodoméstico localizado!";
                    return resposta;
                }

                tipoEletrodomestico.nome_eletrodomestico = tipoEletrodomesticoEditDto.nome_eletrodomestico;
                tipoEletrodomestico.quantidade = tipoEletrodomesticoEditDto.quantidade;

                _context.Update(tipoEletrodomestico);
                await _context.SaveChangesAsync();

                resposta.Dados = tipoEletrodomestico;
                resposta.Mensagem = "Tipo eletrodoméstico editado com sucesso!";
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
