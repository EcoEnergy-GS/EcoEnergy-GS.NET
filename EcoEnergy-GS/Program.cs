using EcoEnergy_GS.Controllers;
using EcoEnergy_GS.Data;
using EcoEnergy_GS.Services.ConsumoEnergia;
using EcoEnergy_GS.Services.Endereco;
using EcoEnergy_GS.Services.HistoricoPontos;
using EcoEnergy_GS.Services.Recompensas;
using EcoEnergy_GS.Services.Residencia;
using EcoEnergy_GS.Services.TipoEletrodomestico;
using EcoEnergy_GS.Services.TrocasRecompensas;
using EcoEnergy_GS.Services.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Configurando a conexão com banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")).EnableSensitiveDataLogging();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeção de dependencia para os Services
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();
builder.Services.AddScoped<IHistoricoPontosInterface, HistoricoPontosService>();
builder.Services.AddScoped<IRecompensasInterface, RecompensasService>();
builder.Services.AddScoped<IEnderecoInterface, EnderecoService>();
builder.Services.AddScoped<ITipoEletrodomesticoInterface, TipoEletrodomesticoService>();
builder.Services.AddScoped<ITrocasRecompensasInterface, TrocasRecompensasService>();
builder.Services.AddScoped<IResidenciaInterface, ResidenciaService>();
builder.Services.AddScoped<IConsumoEnergiaInterface, ConsumoEnergiaService>();

//ML.NET
var dataPath = @"C:\Users\Gabriel\Documents\GS\EcoEnergy-GS\EcoEnergy-GS.IA\Data\DataTrain.json";

var jsonData = File.ReadAllText(dataPath);
var energyData = JsonSerializer.Deserialize<List<EnergyConsumptionData>>(jsonData);

var mlContext = new MLContext();

var dataView = mlContext.Data.LoadFromEnumerable(energyData);

var pipeline = mlContext.Transforms.Conversion
                .MapValueToKey("HoraEncoded", nameof(EnergyConsumptionData.Hora))
                .Append(mlContext.Transforms.Concatenate("Features", nameof(EnergyConsumptionData.Temperatura), nameof(EnergyConsumptionData.ConsumoAtual)))
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(EnergyConsumptionData.ConsumoPrevisto), maximumNumberOfIterations: 100));

var model = pipeline.Fit(dataView);

var modelPath = "model.zip";
mlContext.Model.Save(model, dataView.Schema, modelPath);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }