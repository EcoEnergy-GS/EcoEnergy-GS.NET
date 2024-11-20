using EcoEnergy_GS.DTO.TrocasRecompensas;
using EcoEnergy_GS.Models;

namespace EcoEnergy_GS.Services.TrocasRecompensas
{
    public interface ITrocasRecompensasInterface
    {
        Task<ResponseModel<List<TrocasRecompensasModel>>> ListarTrocasRecompensas();
        Task<ResponseModel<TrocasRecompensasModel>> BucarTrocasRecompensasPorId(int id_trocas);
        Task<ResponseModel<TrocasRecompensasModel>> CreateTrocasRecompensas(TrocasRecompensasCreateDto trocasRecompensasCreateDto);
        Task<ResponseModel<TrocasRecompensasModel>> EditTrocasRecompensas(TrocasRecompensasEditDto trocasRecompensasEditDto);
        Task<ResponseModel<TrocasRecompensasModel>> DeleteTrocasRecompensas(int id_trocas);
    }
}
