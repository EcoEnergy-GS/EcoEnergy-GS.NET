using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.Residencia;
using EcoEnergy_GS.DTO.TrocasRecompensas;
using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy_GS.Services.Residencia
{
    public class ResidenciaService : IResidenciaInterface
    {
        public readonly AppDbContext _context;

        public ResidenciaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<ResidenciaModel>>> ListarResidencia()
        {
            ResponseModel<List<ResidenciaModel>> resposta = new ResponseModel<List<ResidenciaModel>>();

            try
            {
                var residencia = await _context.Residencia.Include(u => u.Usuario).Include(e => e.Endereco).Include(te => te.TipoEletrodomestico).ToListAsync();

                resposta.Dados = residencia;
                resposta.Mensagem = "Todas residencias coletadas!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ResidenciaModel>> BucarResidenciaPorId(int id_residencia)
        {
            ResponseModel<ResidenciaModel> resposta = new ResponseModel<ResidenciaModel>();

            try
            {
                var residencia = await _context.Residencia
                    .Include(u => u.Usuario)
                    .Include(e => e.Endereco)
                    .Include(te => te.TipoEletrodomestico)
                    .FirstOrDefaultAsync(
                        residenciaDb => residenciaDb.id_residencia == id_residencia
                    );

                if (residencia == null)
                {
                    resposta.Mensagem = "Nenhuma residencia localizada!";
                    return resposta;
                }

                resposta.Dados = residencia;
                resposta.Mensagem = "Residencia localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ResidenciaModel>> CreateResidencia(ResidenciaCreateDto residenciaCreateDto)
        {
            ResponseModel<ResidenciaModel> resposta = new ResponseModel<ResidenciaModel>();
            try
            {
                var residenciaDb = await _context.Residencia
                    .Include(u => u.Usuario)
                    .Include(e => e.Endereco)
                    .Include(te => te.TipoEletrodomestico)
                    .FirstOrDefaultAsync(
                        r =>
                        r.Usuario.id_usuarios == residenciaCreateDto.id_usuarios &&
                        r.TipoEletrodomestico.id_eletrodomestico == residenciaCreateDto.id_eletrodomestico &&
                        r.Endereco.id_endereco == residenciaCreateDto.id_endereco 
                    );

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioDb => usuarioDb.id_usuarios == residenciaCreateDto.id_usuarios);

                if (usuario == null)
                {
                    resposta.Mensagem = "Usuário não encontrado!";
                    return resposta;
                }

                var endereco = await _context.Endereco.FirstOrDefaultAsync(enderecoDb => enderecoDb.id_endereco == residenciaCreateDto.id_endereco);

                if (endereco == null)
                {
                    resposta.Mensagem = "Endereço não encontrado!";
                    return resposta;
                }

                var tipoEletrodomestico = await _context.TipoEletrodomestico.FirstOrDefaultAsync(tipoDb => tipoDb.id_eletrodomestico == residenciaCreateDto.id_eletrodomestico);

                if (tipoEletrodomestico == null)
                {
                    resposta.Mensagem = "Tipo de eletrodoméstico não encontrado!";
                    return resposta;
                }

                var residencia = new ResidenciaModel()
                {
                    dispotivico_monitoramento = residenciaCreateDto.dispotivico_monitoramento,
                    quantidade_pessoas = residenciaCreateDto.quantidade_pessoas,
                    media_consumo = residenciaCreateDto.media_consumo,
                    Usuario = usuario,
                    Endereco = endereco,
                    TipoEletrodomestico = tipoEletrodomestico
                };

                _context.Add(residencia);
                _context.SaveChanges();

                resposta.Dados = residencia;
                resposta.Mensagem = "Residencia criada com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Ocorreu um erro ao criar a Troca de recompensa: " + ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ResidenciaModel>> DeleteResidencia(int id_residencia)
        {
            ResponseModel<ResidenciaModel> resposta = new ResponseModel<ResidenciaModel>();

            try
            {
                var residencia = await _context.Residencia
                    .FirstOrDefaultAsync(residenciaDb => residenciaDb.id_residencia == id_residencia);

                if (residencia == null)
                {
                    resposta.Mensagem = "Nenhuma residencia encontrada!";
                }

                _context.Remove(residencia);
                await _context.SaveChangesAsync();

                resposta.Dados = residencia;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ResidenciaModel>> EditResidencia(ResidenciaEditDto residenciaEditDto)
        {
            ResponseModel<ResidenciaModel> resposta = new ResponseModel<ResidenciaModel>();

            try
            {
                var residencia = await _context.Residencia
                    .Include(u => u.Usuario)
                    .Include(e => e.Endereco)
                    .Include(te => te.TipoEletrodomestico)
                    .FirstOrDefaultAsync(
                        residenciaDb => residenciaDb.id_residencia == residenciaEditDto.id_residencia
                    );

                if (residencia == null)
                {
                    resposta.Mensagem = "Nenhum registro de residencia encontrado!";
                    return resposta;
                }

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(usuarioDb => usuarioDb.id_usuarios == residenciaEditDto.id_usuarios);

                if (usuario == null)
                {
                    resposta.Mensagem = "Nenhum registro de usuario encontrado!";
                    return resposta;
                }

                var endereco = await _context.Endereco
                    .FirstOrDefaultAsync(
                        enderecoDb => enderecoDb.id_endereco == residenciaEditDto.id_endereco
                    );

                if( endereco == null)
                {
                    resposta.Mensagem = "Nenhum registro de endereco encontrado!";
                    return resposta;
                }

                var tipoEletrodomestico = await _context.TipoEletrodomestico
                    .FirstOrDefaultAsync(
                        tipoDb => tipoDb.id_eletrodomestico == residenciaEditDto.id_eletrodomestico
                    );

                if(tipoEletrodomestico == null)
                {
                    resposta.Mensagem = "Nenhum registro de tipo eletrodomestico encontrado!";
                    return resposta;
                }

                residencia.dispotivico_monitoramento = residenciaEditDto.dispotivico_monitoramento;
                residencia.quantidade_pessoas = residenciaEditDto.quantidade_pessoas;
                residencia.media_consumo = residenciaEditDto.media_consumo;
                residencia.Usuario = usuario;
                residencia.TipoEletrodomestico = tipoEletrodomestico;
                residencia.Endereco = endereco;

                _context.Update(residencia);
                await _context.SaveChangesAsync();

                resposta.Dados = residencia;
                resposta.Mensagem = "Residencia editada com sucesso";
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
