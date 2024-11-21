using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace EcoEnergy_GS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergyController : ControllerBase
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public EnergyController()
        {
            _mlContext = new MLContext();

            _model = LoadTrainedModel();
        }

        [HttpPost("generate-recommendation")]
        public IActionResult GenerateRecommendation([FromBody] EnergyConsumptionData request)
        {
            if (request == null)
                return BadRequest("Dados de consumo são necessários.");

            var recommendation = GenerateEconomyRecommendation(request);

            var points = CalculatePoints(recommendation);

            return Ok(new
            {
                Recommendation = recommendation,
                Points = points
            });
        }

        private ITransformer LoadTrainedModel()
        {
            var modelPath = "model.zip";
            return _mlContext.Model.Load(modelPath, out var modelInputSchema);
        }

        private float GenerateEconomyRecommendation(EnergyConsumptionData data)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<EnergyConsumptionData, EnergyConsumptionPrediction>(_model);

            var prediction = predictionEngine.Predict(data);

            return (data.ConsumoAtual - prediction.ConsumoPrevisto) * 0.1f;
        }

        private int CalculatePoints(float economy)
        {
            return (int)(economy * 10); 
        }
    }

    public class EnergyConsumptionData
    {
        public float ConsumoAtual { get; set; }
        public float ConsumoPrevisto { get; set; }
        public float Temperatura { get; set; }
        public DateTime Hora { get; set; }
    }

    public class EnergyConsumptionPrediction
    {
        public float ConsumoPrevisto { get; set; }
    }
}
