using EcoEnergy_GS.Data;
using EcoEnergy_GS.Services.Endereco;
using EcoEnergy_GS.Services.HistoricoPontos;
using EcoEnergy_GS.Services.Recompensas;
using EcoEnergy_GS.Services.Residencia;
using EcoEnergy_GS.Services.TipoEletrodomestico;
using EcoEnergy_GS.Services.TrocasRecompensas;
using EcoEnergy_GS.Services.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Configurando a conexão com banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
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