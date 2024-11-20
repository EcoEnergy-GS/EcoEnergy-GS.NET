using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.TipoEletrodomestico;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Tests.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcoEnergy_GS.Tests.Tests
{
    public class TipoEletrodomesticoApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public TipoEletrodomesticoApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        [Fact]
        public async Task GetTipoEletrodomestico_ReturnsListOfTipoEletrodomestico()
        {
            _context.TipoEletrodomestico.Add(new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Geladeira",
                quantidade = 2
            });

            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync("/api/TipoEletrodomestico/ListarTipoEletrodomestico");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<TipoEletrodomesticoModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetTipoEletrodomesticoById_ReturnTipoEletrodomestico()
        {
            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Geladeira",
                quantidade = 2
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync($"/api/TipoEletrodomestico/BucarTipoEletrodomesticoPorId/{tipoEletrodomestico.id_eletrodomestico}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<TipoEletrodomesticoModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetTipoEletrodomesticoById_ReturnNull_WhenDoesntExist()
        {
            //Arrange
            int id_eletrodomestico = 8989;

            //Act
            var response = await _client.GetAsync($"/api/TipoEletrodomestico/BucarTipoEletrodomesticoPorId/{id_eletrodomestico}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<TipoEletrodomesticoModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateTipoEletrodomestico_ReturnsOKTipoEletrodomesticoAndTipoEletrodomestico()
        {
            //Arrange
            var tipoEletrodomestico = new TipoEletrodomesticoCreateDto
            {
                nome_eletrodomestico = "Geladeira",
                quantidade = 2
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/TipoEletrodomestico/CreateTipoEletrodomestico", tipoEletrodomestico);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<TipoEletrodomesticoModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(tipoEletrodomestico.quantidade, json.Dados.quantidade);
        }

        [Fact]
        public async Task CreateTipoEletrodomestico_ReturnNull_WhenNotEnoughtData()
        {
            //Arrange
            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = ""
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/TipoEletrodomestico/CreateTipoEletrodomestico", tipoEletrodomestico);

            //Asserts
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task EditTipoEletrodomestico_ReturnsNoContent_WhenTipoEletrodomesticoExist()
        {
            //Arrange
            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Geladeira",
                quantidade = 2
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            var editedTipoEletrodomestico = new TipoEletrodomesticoModel
            {
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                nome_eletrodomestico = "Geladeira",
                quantidade = 2
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/TipoEletrodomestico/EditTipoEletrodomestico/{tipoEletrodomestico.id_eletrodomestico}", editedTipoEletrodomestico);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditTipoEletrodomestico_ReturnsNoFound_WhenTipoEletrodomesticoDoesntExist()
        {
            //Arrange
            int id_eletrodomestico = 1234;

            var editedTipoEletrodomestico = new TipoEletrodomesticoModel
            {
                id_eletrodomestico = id_eletrodomestico,
                nome_eletrodomestico = "Geladeira",
                quantidade = 2
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/TipoEletrodomestico/EditTipoEletrodomestico/{id_eletrodomestico}", editedTipoEletrodomestico);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTipoEletrodomestico_ReturnsNoContent_WhenTipoEletrodomesticoExist()
        {
            //Arrange
            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Geladeira",
                quantidade = 2
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            //Act
            var response = await _client.DeleteAsync($"/api/TipoEletrodomestico/DeleteTipoEletrodomestico/{tipoEletrodomestico.id_eletrodomestico}");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTipoEletrodomestico_ReturnsNoContent_WhenTipoEletrodomesticoDoesntExist()
        {
            //Arrange
            var id_TipoEletrodomestico = 1234;

            //Act
            var response = await _client.DeleteAsync($"/api/TipoEletrodomestico/DeleteTipoEletrodomestico/{id_TipoEletrodomestico}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }   
    }
}
