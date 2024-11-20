using EcoEnergy_GS.DTO.ConsumoEnergia;
using EcoEnergy_GS.DTO.HistoricoPontos;
using EcoEnergy_GS.Models;

namespace EcoEnergy_GS.Services.ConsumoEnergia
{
    public interface IConsumoEnergiaInterface
    {
        Task<ResponseModel<List<ConsumoEnergiaModel>>> ListarConsumoEnergia();
        Task<ResponseModel<ConsumoEnergiaModel>> BucarConsumoEnergiaPorId(int id_consumo);
        Task<ResponseModel<ConsumoEnergiaModel>> CreateConsumoEnergia(ConsumoEnergiaCreateDto consumoEnergiaCreateDto);
        Task<ResponseModel<ConsumoEnergiaModel>> EditConsumoEnergia(ConsumoEnergiaEditDto consumoEnergiaEditDto);
        Task<ResponseModel<ConsumoEnergiaModel>> DeleteConsumoEnergia(int id_consumo);
    }
}
