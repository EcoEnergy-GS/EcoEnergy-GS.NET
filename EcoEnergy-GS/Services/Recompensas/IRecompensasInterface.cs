using EcoEnergy_GS.DTO.Recompensas;
using EcoEnergy_GS.Models;

namespace EcoEnergy_GS.Services.Recompensas
{
    public interface IRecompensasInterface
    {
        Task<ResponseModel<List<RecompensasModel>>> ListarRecompensas();
        Task<ResponseModel<RecompensasModel>> BucarRecompensasPorId(int id_recompensas);
        Task<ResponseModel<RecompensasModel>> CreateRecompensas(RecompensasCreateDto recompensasCreateDto);
        Task<ResponseModel<RecompensasModel>> EditRecompensas(RecompensasEditDto recompensasEditDto);
        Task<ResponseModel<RecompensasModel>> DeleteRecompensas(int id_recompensas);
    }
}
