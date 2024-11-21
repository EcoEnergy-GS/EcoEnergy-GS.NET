using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.Endereco;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Tests.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace EcoEnergy_GS.Tests.Tests
{
    public class EnderecoApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public EnderecoApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        [Fact]
        public async Task GetEnderecos_ReturnsListOfEnderecos()
        {
            _context.Endereco.Add(new EnderecoModel
            {
                cep = "01212111",
                rua = "Rua de Teste",
                numero = 21,
                complemento = "Apartamento 123"
            });

            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync("/api/Endereco/ListarEndereco");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<EnderecoModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetEnderecosById_ReturnEndereco()
        {
            var endereco = new EnderecoModel
            {
                cep = "01212111",
                rua = "Rua de Teste",
                numero = 21,
                complemento = "Apartamento 123"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync($"/api/Endereco/BucarEnderecoPorId/{endereco.id_endereco}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<EnderecoModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetEnderecoById_ReturnNull_WhenDoesntExist()
        {
            //Arrange
            int id_endereco = 8989;

            //Act
            var response = await _client.GetAsync($"/api/Endereco/BucarEnderecoPorId/{id_endereco}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<EnderecoModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateEndereco_ReturnsOKEnderecoAndEndereco()
        {
            //Arrange
            var endereco = new EnderecoCreateDto
            {
                cep = "01212111",
                rua = "Rua de Teste",
                numero = 21,
                complemento = "Apartamento 123"
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Endereco/CreateEndereco", endereco);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<EnderecoModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(endereco.cep, json.Dados.cep);
        }

        [Fact]
        public async Task CreateEndereco_ReturnNull_WhenNotEnoughtData()
        {
            //Arrange
            var endereco = new EnderecoModel
            {
                cep = "01212111"
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Endereco/CreateEndereco", endereco);

            //Asserts
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task EditEndereco_ReturnsNoContent_WhenEnderecoExist()
        {
            //Arrange
            var endereco = new EnderecoModel
            {
                cep = "01212111",
                rua = "Rua de Teste",
                numero = 21,
                complemento = "Apartamento 123"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            var editedEndereco = new EnderecoModel
            {
                id_endereco = endereco.id_endereco,
                cep = "12312345",
                rua = "Av. de Teste",
                numero = 25,
                complemento = "Apartamento 456"
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/Endereco/EditEndereco/{endereco.id_endereco}", editedEndereco);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditEndereco_ReturnsNoFound_WhenEnderecoDoesntExist()
        {
            //Arrange
            int id_Endereco = 1234;

            var editedEndereco = new EnderecoModel
            {
                id_endereco = id_Endereco,
                cep = "12312345",
                rua = "Av. de Teste",
                numero = 25,
                complemento = "Apartamento 456"
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/Endereco/EditEndereco/{id_Endereco}", editedEndereco);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteEndereco_ReturnsNoContent_WhenEnderecoExist()
        {
            //Arrange
            var endereco = new EnderecoModel
            {
                cep = "12312345",
                rua = "Av. de Teste",
                numero = 25,
                complemento = "Apartamento 456"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            //Act
            var response = await _client.DeleteAsync($"/api/Endereco/DeleteEndereco/{endereco.id_endereco}");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteEndereco_ReturnsNoContent_WhenEnderecoDoesntExist()
        {
            //Arrange
            var id_endereco = 1234;

            //Act
            var response = await _client.DeleteAsync($"/api/Endereco/DeleteEndereco/{id_endereco}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

