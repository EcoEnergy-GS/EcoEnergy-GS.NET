using Microsoft.AspNetCore.Mvc;
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

            var (economy, recommendation) = GenerateEconomyRecommendation(request);

            var points = CalculatePoints(economy, request.ConsumoAtual);

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

        private (float economy, string recommendation) GenerateEconomyRecommendation(EnergyConsumptionData data)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<EnergyConsumptionData, EnergyConsumptionPrediction>(_model);

            var prediction = predictionEngine.Predict(data);

            // Calculando a economia de energia
            var economy = data.ConsumoAtual - prediction.ConsumoPrevisto;

            // Gerando uma recomendação textual baseada na economia
            string recommendation;
            if (economy > 0)
            {
                recommendation = "Você economizou energia! Continue assim para reduzir ainda mais o seu consumo.";
            }
            else if (economy < 0)
            {
                recommendation = "O consumo está acima do esperado. Tente reduzir o uso de energia para evitar desperdícios.";
            }
            else
            {
                recommendation = "Seu consumo está dentro do esperado. Mantenha o controle para evitar desperdícios.";
            }

            // Retornando tanto a economia (float) quanto a recomendação (string)
            return (economy, recommendation);
        }


        private int CalculatePoints(float economy, float totalConsumption)
        {
            float percentage = (economy / totalConsumption) * 100;
            return (int)Math.Round(percentage);
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