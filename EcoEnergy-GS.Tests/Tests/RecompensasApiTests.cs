using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.Recompensas;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Tests.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace EcoEnergy_GS.Tests.Tests
{
    public class RecompensasApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public RecompensasApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        [Fact]
        public async Task GetRecompensas_ReturnsListOfRecompensas()
        {
            _context.Recompensas.Add(new RecompensasModel
            {
                descricao = "desc",
                pontos_necessarios = 2
            });


            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync("/api/Recompensas/ListarRecompensas");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<RecompensasModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetRecompensasById_ReturnRecompensas()
        {
            var recompensas = new RecompensasModel
            {
                descricao = "desc",
                pontos_necessarios = 2
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync($"/api/Recompensas/BucarRecompensasPorId/{recompensas.id_recompensas}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<RecompensasModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetRecompensasById_ReturnNull_WhenDoesntExist()
        {
            //Arrange
            int id_Recompensas = 8989;

            //Act
            var response = await _client.GetAsync($"/api/Recompensas/BucarRecompensasPorId/{id_Recompensas}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<RecompensasModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateRecompensas_ReturnsOKRecompensasAndRecompensas()
        {
            //Arrange
            var recompensas = new RecompensasCreateDto
            {
                descricao = "Descrição do produto",
                pontos_necessarios = 100
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Recompensas/CreateRecompensas", recompensas);

            //Assert
            //response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<RecompensasModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(recompensas.descricao, json.Dados.descricao);
        }

        [Fact]
        public async Task EditRecompensas_ReturnsNoContent_WhenRecompensasExist()
        {
            //Arrange
            var recompensas = new RecompensasModel
            {
                descricao = "Descrição do produto",
                pontos_necessarios = 2
            };

            _context.Recompensas.Add(recompensas);
            await _context.SaveChangesAsync();

            var editedRecompensas = new RecompensasModel
            {
                id_recompensas = recompensas.id_recompensas,
                descricao = "Descrição do produto2",
                pontos_necessarios = 4
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/Recompensas/EditRecompensas/{recompensas.id_recompensas}", editedRecompensas);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditRecompensas_ReturnsNoFound_WhenRecompensasDoesntExist()
        {
            //Arrange
            int id_recompensas = 1234;

            var editedRecompensas = new RecompensasModel
            {
                id_recompensas = id_recompensas,
                descricao = "Descrição do produto",
                pontos_necessarios = 2
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/Recompensas/EditRecompensas/{id_recompensas}", editedRecompensas);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteRecompensas_ReturnsNoContent_WhenRecompensasExist()
        {
            //Arrange
            var recompensas = new RecompensasModel
            {
                descricao = "desc",
                pontos_necessarios = 2
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            //Act
            var response = await _client.DeleteAsync($"/api/Recompensas/DeleteRecompensas/{recompensas.id_recompensas}");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteRecompensas_ReturnsNoContent_WhenRecompensasDoesntExist()
        {
            //Arrange
            var id_Recompensas = 1234;

            //Act
            var response = await _client.DeleteAsync($"/api/Recompensas/DeleteRecompensas/{id_Recompensas}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
