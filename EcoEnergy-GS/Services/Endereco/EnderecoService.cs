using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.Endereco;
using EcoEnergy_GS.DTO.Usuarios;
using EcoEnergy_GS.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;

namespace EcoEnergy_GS.Services.Endereco
{
    public class EnderecoService : IEnderecoInterface
    {
        public readonly AppDbContext _context;

        public EnderecoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<EnderecoModel>> BucarEnderecoPorId(int id_endereco)
        {
            ResponseModel<EnderecoModel> resposta = new ResponseModel<EnderecoModel>();

            try
            {
                var endereco = await _context.Endereco.FirstOrDefaultAsync(enderecoBanco => enderecoBanco.id_endereco == id_endereco);

                if (endereco == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado!";
                    return resposta;
                }

                resposta.Dados = endereco;
                resposta.Mensagem = "Endereço localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<EnderecoModel>>> ListarEndereco()
        {
            ResponseModel<List<EnderecoModel>> resposta = new ResponseModel<List<EnderecoModel>>();

            try
            {
                var endereco = await _context.Endereco.ToListAsync();

                resposta.Dados = endereco;
                resposta.Mensagem = "Todos os endereços coletados!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<EnderecoModel>> CreateEndereco(EnderecoCreateDto enderecoCreateDto)
        {
            ResponseModel<EnderecoModel> resposta = new ResponseModel<EnderecoModel>();

            try
            {
                var endereco = new EnderecoModel()
                {
                    cep = enderecoCreateDto.cep,
                    rua = enderecoCreateDto.rua,
                    numero = enderecoCreateDto.numero,
                    complemento = enderecoCreateDto.complemento
                };

                _context.Add(endereco);
                await _context.SaveChangesAsync();

                resposta.Dados = endereco;
                resposta.Mensagem = "Endereço criado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<EnderecoModel>> DeleteEndereco(int id_endereco)
        {
            ResponseModel<EnderecoModel> resposta = new ResponseModel<EnderecoModel>();

            try
            {
                var endereco = await _context.Endereco.FirstOrDefaultAsync(enderecoBanco => enderecoBanco.id_endereco == id_endereco);

                if (endereco == null)
                {
                    resposta.Mensagem = "Nenhum Endereço localizado!";
                    return resposta;
                }

                _context.Remove(endereco);
                await _context.SaveChangesAsync();

                resposta.Dados = endereco;
                resposta.Mensagem = "Endereço deletado com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<EnderecoModel>> EditEndereco(EnderecoEditDto enderecoEditDto)
        {
            ResponseModel<EnderecoModel> resposta = new ResponseModel<EnderecoModel>();

            try
            {
                var endereco = await _context.Endereco
                    .FirstOrDefaultAsync(
                    enderecoBanco => enderecoBanco.id_endereco == enderecoEditDto.id_endereco);

                if (endereco == null)
                {
                    resposta.Mensagem = "Nenhum endereço localizado!";
                    return resposta;
                }

                endereco.cep = enderecoEditDto.cep;
                endereco.rua = enderecoEditDto.rua;
                endereco.numero = enderecoEditDto.numero;
                endereco.complemento = enderecoEditDto.complemento;

                _context.Update(endereco);
                await _context.SaveChangesAsync();

                resposta.Dados = endereco;
                resposta.Mensagem = "Endereço editado com sucesso!";
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
