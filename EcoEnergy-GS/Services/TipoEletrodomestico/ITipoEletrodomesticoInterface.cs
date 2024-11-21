using EcoEnergy_GS.DTO.TipoEletrodomestico;
using EcoEnergy_GS.Models;

namespace EcoEnergy_GS.Services.TipoEletrodomestico
{
    public interface ITipoEletrodomesticoInterface
    {
        Task<ResponseModel<List<TipoEletrodomesticoModel>>> ListarTipoEletrodomestico();
        Task<ResponseModel<TipoEletrodomesticoModel>> BucarTipoEletrodomesticoPorId(int id_eletrodomestico);
        Task<ResponseModel<TipoEletrodomesticoModel>> CreateTipoEletrodomestico(TipoEletrodomesticoCreateDto tipoEletrodomesticoCreateDto);
        Task<ResponseModel<TipoEletrodomesticoModel>> EditTipoEletrodomestico(TipoEletrodomesticoEditDto tipoEletrodomesticoEditDto);
        Task<ResponseModel<TipoEletrodomesticoModel>> DeleteTipoEletrodomestico(int id_eletrodomestico);
    }
}
