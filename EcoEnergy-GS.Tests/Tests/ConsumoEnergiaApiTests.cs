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
    public class ConsumoEnergiaApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public ConsumoEnergiaApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        [Fact]
        public async Task GetConsumoEnergia_ReturnsListOfConsumoEnergia()
        {
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

            var consumo = new ConsumoEnergiaModel
            {
                data_consumo = DateTime.Now,
                consumo = 3,
                id_residencia = residencia.id_residencia
            };

            _context.ConsumoEnergia.Add(consumo);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync("/api/ConsumoEnergia/ListarConsumoEnergia");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<ConsumoEnergiaModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetByIdConsumoEnergia_ReturnConsumoEnergia()
        {
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

            var consumo = new ConsumoEnergiaModel
            {
                data_consumo = DateTime.Now,
                consumo = 3,
                id_residencia = residencia.id_residencia
            };

            _context.ConsumoEnergia.Add(consumo);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync($"/api/ConsumoEnergia/BucarConsumoEnergiaPorId/{consumo.id_consumo}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ConsumoEnergiaModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetConsumoEnergiaById_ReturnNull_WhenDoesntExist()
        {
            //Arrange
            int id_consumo = 8989;

            //Act
            var response = await _client.GetAsync($"/api/ConsumoEnergia/BucarConsumoEnergiaPorId/{id_consumo}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ConsumoEnergiaModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateConsumoEnergia_ReturnsOKUserAndConsumoEnergia()
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

            var consumo = new ConsumoEnergiaModel
            {
                data_consumo = DateTime.Now,
                consumo = 3,
                id_residencia = residencia.id_residencia
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/ConsumoEnergia/CreateConsumoEnergia", consumo);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ConsumoEnergiaModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(consumo.data_consumo, json.Dados.data_consumo);
        }

        [Fact]
        public async Task CreateConsumoEnergia_ReturnsNull_WhenResidenciaDoesntExist()
        {
            //Arrange
            var consumo = new ConsumoEnergiaModel
            {
                data_consumo = DateTime.Now,
                consumo = 3,
                id_residencia = 1
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/ConsumoEnergia/CreateConsumoEnergia", consumo);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ConsumoEnergiaModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task EditConsumoEnergia_ReturnsNoContent_WhenConsumoEnergiaPontosExist()
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

            var consumo = new ConsumoEnergiaModel
            {
                data_consumo = DateTime.Now,
                consumo = 3,
                id_residencia = residencia.id_residencia
            };

            _context.ConsumoEnergia.Add(consumo);
            _context.SaveChanges();

            var editedConsumo = new ConsumoEnergiaModel
            {
                id_consumo = consumo.id_consumo,
                data_consumo = DateTime.Now,
                consumo = 5,
                id_residencia = residencia.id_residencia
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/ConsumoEnergia/EditConsumoEnergia/{consumo.id_consumo}", editedConsumo);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditHConsumoEnergia_ReturnsNotFound_WhenConsumoEnergiaDoesntExist()
        {
            //Arrange
            int id_consumo = 1234;

            var editedConsumo = new ConsumoEnergiaModel
            {
                id_consumo = id_consumo,
                data_consumo = DateTime.Now,
                consumo = 5,
                id_residencia = 2
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/ConsumoEnergia/EditConsumoEnergia/{id_consumo}", editedConsumo);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteConsumoEnergia_ReturnsNoContent_WhenConsumoEnergiaExist()
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

            var consumo = new ConsumoEnergiaModel
            {
                data_consumo = DateTime.Now,
                consumo = 3,
                id_residencia = residencia.id_residencia
            };

            _context.ConsumoEnergia.Add(consumo);
            _context.SaveChanges();

            //Act
            var response = await _client.DeleteAsync($"/api/ConsumoEnergia/DeleteConsumoEnergia/{consumo.id_consumo}");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteConsumoEnergia_ReturnsNotFound_WhenConsumoEnergiaDoesntExist()
        {
            //Arrange
            var id_consumo = 1234;

            //Act
            var response = await _client.DeleteAsync($"/api/ConsumoEnergia/DeleteConsumoEnergia/{id_consumo}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
