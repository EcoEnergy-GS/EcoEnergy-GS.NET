using EcoEnergy_GS.DTO.Endereco;
using EcoEnergy_GS.Models;

namespace EcoEnergy_GS.Services.Endereco
{
    public interface IEnderecoInterface
    {
        Task<ResponseModel<List<EnderecoModel>>> ListarEndereco();
        Task<ResponseModel<EnderecoModel>> BucarEnderecoPorId(int id_endereco);
        Task<ResponseModel<EnderecoModel>> CreateEndereco(EnderecoCreateDto enderecoCreateDto);
        Task<ResponseModel<EnderecoModel>> EditEndereco(EnderecoEditDto enderecoEditDto);
        Task<ResponseModel<EnderecoModel>> DeleteEndereco(int id_endereco);
    }
}
