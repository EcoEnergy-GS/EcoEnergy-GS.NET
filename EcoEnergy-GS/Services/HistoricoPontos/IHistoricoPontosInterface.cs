using EcoEnergy_GS.DTO.HistoricoPontos;
using EcoEnergy_GS.Models;

namespace EcoEnergy_GS.Services.HistoricoPontos
{
    public interface IHistoricoPontosInterface
    {
        Task<ResponseModel<List<HistoricoPontosModel>>> ListarHistorico();
        Task<ResponseModel<HistoricoPontosModel>> BucarHistoricoPorId(int id_historico);
        Task<ResponseModel<HistoricoPontosModel>> CreateHistorico(HistoricoPontosCreateDto historicoCreateDto);
        Task<ResponseModel<HistoricoPontosModel>> EditHistorico(HistoricoPontosEditDto historicoEditDto);
        Task<ResponseModel<HistoricoPontosModel>> DeleteHistorico(int id_historico);
    }
}
