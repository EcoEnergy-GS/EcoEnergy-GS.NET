using EcoEnergy_GS.Data;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Tests.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EcoEnergy_GS.Tests.Tests
{
    public class ResidenciaApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public ResidenciaApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }


        [Fact]
        public async Task GetResidencia_ReturnsListOfResidencia()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var endereco = new EnderecoModel
            {
                cep = "11111111",
                rua = "Rua de testes",
                numero = 21,
                complemento = "Apartamento 49"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Ar-condicionado",
                quantidade = 3
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            var residencia = new ResidenciaModel
            {
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                id_usuarios = user.id_usuarios,
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                id_endereco = endereco.id_endereco
            };

            _context.Residencia.Add(residencia);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync("/api/Residencia/ListarResidencia");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<ResidenciaModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetByIdResidencia_ReturnResidencia()
        {
            //Act
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var endereco = new EnderecoModel
            {
                cep = "11111111",
                rua = "Rua de testes",
                numero = 21,
                complemento = "Apartamento 49"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Ar-condicionado",
                quantidade = 3
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            var residencia = new ResidenciaModel
            {
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                id_usuarios = user.id_usuarios,
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                id_endereco = endereco.id_endereco
            };

            _context.Residencia.Add(residencia);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync($"/api/Residencia/BucarResidenciaPorId/{residencia.id_residencia}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ResidenciaModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetTrocasResidenciaById_ReturnNull_WhenDoesntExist()
        {
            //Arrange
            int id_residencia = 8989;

            //Act
            var response = await _client.GetAsync($"/api/Residencia/BucarResidenciaPorId/{id_residencia}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ResidenciaModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateResidencia_ReturnsOKResidenciaAndResidencia()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var endereco = new EnderecoModel
            {
                cep = "11111111",
                rua = "Rua de testes",
                numero = 21,
                complemento = "Apartamento 49"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Ar-condicionado",
                quantidade = 3
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            var residencia = new ResidenciaModel
            {
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                id_usuarios = user.id_usuarios,
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                id_endereco = endereco.id_endereco
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Residencia/CreateResidencia", residencia);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ResidenciaModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(residencia.dispotivico_monitoramento, json.Dados.dispotivico_monitoramento);
        }

        [Fact]
        public async Task CreateResidencia_ReturnsNull_WhenUserDoesntExist()
        {
            //Arrange
            //var user = new UsuarioModel
            //{
            //    nome = "Gabriel",
            //    senha = "Gabriel123@",
            //    telefone = "11123456789",
            //    pontos = 12
            //};

            //_context.Usuarios.Add(user);
            //_context.SaveChanges();

            var endereco = new EnderecoModel
            {
                cep = "11111111",
                rua = "Rua de testes",
                numero = 21,
                complemento = "Apartamento 49"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Ar-condicionado",
                quantidade = 3
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            var residencia = new ResidenciaModel
            {
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                //id_usuarios = user.id_usuarios,
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                id_endereco = endereco.id_endereco
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Residencia/CreateResidencia", residencia);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ResidenciaModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateResidencia_ReturnsNull_WhenEnderecoDoesntExist()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            //var endereco = new EnderecoModel
            //{
            //    cep = "11111111",
            //    rua = "Rua de testes",
            //    numero = 21,
            //    complemento = "Apartamento 49"
            //};

            //_context.Endereco.Add(endereco);
            //_context.SaveChanges();

            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Ar-condicionado",
                quantidade = 3
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            var residencia = new ResidenciaModel
            {
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                id_usuarios = user.id_usuarios,
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                //id_endereco = endereco.id_endereco
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Residencia/CreateResidencia", residencia);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ResidenciaModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateResidencia_ReturnsNull_WhenTipoEletrodomesticoDoesntExist()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var endereco = new EnderecoModel
            {
                cep = "11111111",
                rua = "Rua de testes",
                numero = 21,
                complemento = "Apartamento 49"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            //var tipoEletrodomestico = new TipoEletrodomesticoModel
            //{
            //    nome_eletrodomestico = "Ar-condicionado",
            //    quantidade = 3
            //};

            //_context.TipoEletrodomestico.Add(tipoEletrodomestico);
            //_context.SaveChanges();

            var residencia = new ResidenciaModel
            {
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                id_usuarios = user.id_usuarios,
                //id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                id_endereco = endereco.id_endereco
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Residencia/CreateResidencia", residencia);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ResidenciaModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task EditResidencia_ReturnsNoContent_WhenResidenciaExist()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var endereco = new EnderecoModel
            {
                cep = "11111111",
                rua = "Rua de testes",
                numero = 21,
                complemento = "Apartamento 49"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Ar-condicionado",
                quantidade = 3
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            var residencia = new ResidenciaModel
            {
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                id_usuarios = user.id_usuarios,
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                id_endereco = endereco.id_endereco
            };

            _context.Residencia.Add(residencia);
            _context.SaveChanges();

            var residenciaEdited = new ResidenciaModel
            {
                id_residencia = residencia.id_residencia,
                dispotivico_monitoramento = "Dispositivo de monitoramento Update",
                quantidade_pessoas = 4,
                media_consumo = 48,
                id_usuarios = user.id_usuarios,
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                id_endereco = endereco.id_endereco
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/Residencia/EditResidencia/{residencia.id_residencia}", residenciaEdited);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditResidencia_ReturnsNotFound_WhenResidenciaDoesntExist()
        {
            //Arrange
            int id_residencia = 1234;

            var residencia = new ResidenciaModel
            {
                id_residencia = id_residencia,
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                id_usuarios = 1,
                id_eletrodomestico = 1,
                id_endereco = 1
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/Residencia/EditResidencia/{id_residencia}", residencia);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteResidencia_ReturnsNoContent_WhenResidenciaExist()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var endereco = new EnderecoModel
            {
                cep = "11111111",
                rua = "Rua de testes",
                numero = 21,
                complemento = "Apartamento 49"
            };

            _context.Endereco.Add(endereco);
            _context.SaveChanges();

            var tipoEletrodomestico = new TipoEletrodomesticoModel
            {
                nome_eletrodomestico = "Ar-condicionado",
                quantidade = 3
            };

            _context.TipoEletrodomestico.Add(tipoEletrodomestico);
            _context.SaveChanges();

            var residencia = new ResidenciaModel
            {
                dispotivico_monitoramento = "Dispositivo de monitoramento",
                quantidade_pessoas = 3,
                media_consumo = 44,
                id_usuarios = user.id_usuarios,
                id_eletrodomestico = tipoEletrodomestico.id_eletrodomestico,
                id_endereco = endereco.id_endereco
            };

            _context.Residencia.Add(residencia);
            _context.SaveChanges();

            //Act
            var response = await _client.DeleteAsync($"/api/Residencia/DeleteResidencia/{residencia.id_residencia}");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteResidencia_ReturnsNotFound_WhenResidenciaDoesntExist()
        {
            //Arrange
            var id_residencia = 1234;

            //Act
            var response = await _client.DeleteAsync($"/api/Residencia/DeleteResidencia/{id_residencia}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
