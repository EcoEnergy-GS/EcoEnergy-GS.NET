using EcoEnergy_GS.DTO.Residencia;
using EcoEnergy_GS.Models;

namespace EcoEnergy_GS.Services.Residencia
{
    public interface IResidenciaInterface
    {
        Task<ResponseModel<List<ResidenciaModel>>> ListarResidencia();
        Task<ResponseModel<ResidenciaModel>> BucarResidenciaPorId(int id_residencia);
        Task<ResponseModel<ResidenciaModel>> CreateResidencia(ResidenciaCreateDto residenciaCreateDto);
        Task<ResponseModel<ResidenciaModel>> EditResidencia(ResidenciaEditDto residenciaEditDto);
        Task<ResponseModel<ResidenciaModel>> DeleteResidencia(int id_residencia);
    }
}
