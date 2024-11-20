using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.ConsumoEnergia;
using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy_GS.Services.ConsumoEnergia
{
    public class ConsumoEnergiaService : IConsumoEnergiaInterface
    {
        public readonly AppDbContext _context;

        public ConsumoEnergiaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<ConsumoEnergiaModel>>> ListarConsumoEnergia()
        {
            ResponseModel<List<ConsumoEnergiaModel>> resposta = new ResponseModel<List<ConsumoEnergiaModel>>();

            try
            {
                var consumo = await _context.ConsumoEnergia
                    .Include(r => r.Residencia)
                    .Include(u => u.Residencia.Usuario)
                    .Include(te => te.Residencia.TipoEletrodomestico)
                    .Include(e => e.Residencia.Endereco)
                    .ToListAsync();

                resposta.Dados = consumo;
                resposta.Mensagem = "Todos consumo de energia de pontos coletados!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ConsumoEnergiaModel>> BucarConsumoEnergiaPorId(int id_consumo)
        {
            ResponseModel<ConsumoEnergiaModel> resposta = new ResponseModel<ConsumoEnergiaModel>();

            try
            {
                var consumo = await _context.ConsumoEnergia.Include(r => r.Residencia).FirstOrDefaultAsync(consumoDb => consumoDb.id_consumo == id_consumo);

                if (consumo == null)
                {
                    resposta.Mensagem = "Nenhum consumo de energia localizado!";
                    return resposta;
                }

                resposta.Dados = consumo;
                resposta.Mensagem = "Histórico de consumo de energia localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ConsumoEnergiaModel>> CreateConsumoEnergia(ConsumoEnergiaCreateDto consumoEnergiaCreateDto)
        {
            ResponseModel<ConsumoEnergiaModel> resposta = new ResponseModel<ConsumoEnergiaModel>();
            try
            {
                var consumoDb = await _context.ConsumoEnergia
                    //.AsNoTracking()
                    .Include(r => r.Residencia)
                    .FirstOrDefaultAsync(
                        r =>
                        r.Residencia.id_residencia == consumoEnergiaCreateDto.id_residencia
                    );

                var residencia = await _context.Residencia.FirstOrDefaultAsync(residenciaDb => residenciaDb.id_residencia == consumoEnergiaCreateDto.id_residencia);

                if (residencia == null)
                {
                    resposta.Mensagem = "Residência não encontrada!";
                    return resposta;
                }

                var consumo = new ConsumoEnergiaModel()
                {
                    id_residencia = consumoEnergiaCreateDto.id_residencia,
                    data_consumo = consumoEnergiaCreateDto.data_consumo,
                    consumo = consumoEnergiaCreateDto.consumo,
                    Residencia = residencia
                };

                _context.Add(consumo);
                await _context.SaveChangesAsync();

                resposta.Dados = consumo;
                resposta.Mensagem = "Consumo de energia criado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Ocorreu um erro ao criar a consumo de energia: " + ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ConsumoEnergiaModel>> DeleteConsumoEnergia(int id_consumo)
        {
            ResponseModel<ConsumoEnergiaModel> resposta = new ResponseModel<ConsumoEnergiaModel>();
            try
            {
                var consumo = await _context.ConsumoEnergia
                    .FirstOrDefaultAsync(consumoDb => consumoDb.id_consumo == id_consumo);

                if (consumo == null)
                {
                    resposta.Mensagem = "Nenhum consumo de energia encontrado!";
                    return resposta;
                }

                _context.Remove(consumo);
                await _context.SaveChangesAsync();

                resposta.Dados = consumo;
                resposta.Mensagem = "Consumo de energia removido com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ConsumoEnergiaModel>> EditConsumoEnergia(ConsumoEnergiaEditDto consumoEnergiaEditDto)
        {
            ResponseModel<ConsumoEnergiaModel> resposta = new ResponseModel<ConsumoEnergiaModel>();
            try
            {
                var consumo = await _context.ConsumoEnergia
                    .Include(r => r.Residencia)
                    .FirstOrDefaultAsync(
                        consumoDb => consumoDb.id_consumo == consumoEnergiaEditDto.id_consumo
                    );

                if (consumo == null)
                {
                    resposta.Mensagem = "Nenhum registro de consumo de energia localizado!";
                    return resposta;
                }

                var residencia = await _context.Residencia
                    .FirstOrDefaultAsync(residenciaDb => residenciaDb.id_residencia == consumoEnergiaEditDto.id_residencia);

                if (residencia == null)
                {
                    resposta.Mensagem = "Nenhum registro de residencia localizado!";
                    return resposta;
                }

                consumo.data_consumo = consumoEnergiaEditDto.data_consumo;
                consumo.consumo = consumoEnergiaEditDto.consumo;
                consumo.Residencia = residencia;

                resposta.Mensagem = "Consumo de energia editado com sucesso!";
                resposta.Status = true;

                _context.Update(consumo);
                await _context.SaveChangesAsync();

                resposta.Dados = consumo;
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
