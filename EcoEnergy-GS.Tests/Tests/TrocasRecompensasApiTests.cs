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
    public class TrocasRecompensasApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public TrocasRecompensasApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        [Fact]
        public async Task GetTrocasRecompensas_ReturnsListOfTrocasRecompensas()
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

            var recompensas = new RecompensasModel
            {
                descricao = "Descrição de recompensas",
                pontos_necessarios = 12
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            var trocasRecompensas = new TrocasRecompensasModel
            {
                data_troca = DateTime.Now,
                pontos_utilizados = 12,
                id_recompensas = recompensas.id_recompensas,
                id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.GetAsync("/api/TrocasRecompensas/ListarTrocasRecompensas");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<TrocasRecompensasModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetByIdTrocasRecompensas_ReturnTrocasRecompensas()
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

            var recompensas = new RecompensasModel
            {
                descricao = "Descrição de recompensas",
                pontos_necessarios = 12
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            var trocasRecompensas = new TrocasRecompensasModel
            {
                data_troca = DateTime.Now,
                pontos_utilizados = 12,
                id_recompensas = recompensas.id_recompensas,
                id_usuarios = user.id_usuarios
            };

            _context.TrocasRecompensas.Add(trocasRecompensas);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync($"/api/TrocasRecompensas/BucarTrocasRecompensasPorId/{trocasRecompensas.id_trocas}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<TrocasRecompensasModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetTrocasRecompensassById_ReturnNull_WhenDoesntExist()
        {
            //Arrange
            int id_trocas = 8989;

            //Act
            var response = await _client.GetAsync($"/api/TrocasRecompensas/BucarTrocasRecompensasPorId/{id_trocas}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<TrocasRecompensasModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateTrocasRecompensas_ReturnsOKTrocasRecompensasAndTrocasRecompensas()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 300
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var recompensas = new RecompensasModel
            {
                descricao = "Descrição de recompensas",
                pontos_necessarios = -300
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            var trocasRecompensas = new TrocasRecompensasModel
            {
                data_troca = DateTime.Now,
                pontos_utilizados = 300,
                id_recompensas = recompensas.id_recompensas,
                id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/TrocasRecompensas/CreateTrocasRecompensas", trocasRecompensas);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<TrocasRecompensasModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(trocasRecompensas.data_troca, json.Dados.data_troca);
        }

        [Fact]
        public async Task CreateTrocasRecompensas_ReturnsNull_WhenUserDoesntExist()
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

            var recompensas = new RecompensasModel
            {
                descricao = "Descrição de recompensas",
                pontos_necessarios = -12
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            var trocasRecompensas = new TrocasRecompensasModel
            {
                data_troca = DateTime.Now,
                pontos_utilizados = 12,
                id_recompensas = recompensas.id_recompensas,
                //id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/TrocasRecompensas/CreateTrocasRecompensas", trocasRecompensas);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<TrocasRecompensasModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateTrocasRecompensas_ReturnsNull_WhenRecompensasDoesntExist()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 300
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            //var recompensas = new RecompensasModel
            //{
            //    descricao = "Descrição de recompensas",
            //    pontos_necessarios = 12
            //};

            //_context.Recompensas.Add(recompensas);
            //_context.SaveChanges();

            var trocasRecompensas = new TrocasRecompensasModel
            {
                data_troca = DateTime.Now,
                pontos_utilizados = 300,
                //id_recompensas = recompensas.id_recompensas,
                id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/TrocasRecompensas/CreateTrocasRecompensas", trocasRecompensas);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<TrocasRecompensasModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task EditHistoricoPontos_ReturnsNoContent_WhenHistoricoPontosExist()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 300
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var recompensas = new RecompensasModel
            {
                descricao = "Descrição de recompensas",
                pontos_necessarios = -300
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            var trocasRecompensas = new TrocasRecompensasModel
            {
                data_troca = DateTime.Now,
                pontos_utilizados = 300,
                id_recompensas = recompensas.id_recompensas,
                id_usuarios = user.id_usuarios
            };

            _context.TrocasRecompensas.Add(trocasRecompensas);
            _context.SaveChanges();

            var EditedTrocasRecompensas = new TrocasRecompensasModel
            {
                id_trocas = trocasRecompensas.id_trocas,
                data_troca = DateTime.Now,
                pontos_utilizados = 300,
                id_recompensas = recompensas.id_recompensas,
                id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/TrocasRecompensas/EditTrocasRecompensas/{trocasRecompensas.id_trocas}", EditedTrocasRecompensas);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditTrocasRecompensas_ReturnsNotFound_WhenTrocasRecompensasDoesntExist()
        {
            //Arrange
            int id_trocas = 1234;

            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var recompensas = new RecompensasModel
            {
                descricao = "Descrição de recompensas",
                pontos_necessarios = 12
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            var EditedTrocasRecompensas = new TrocasRecompensasModel
            {
                id_trocas = id_trocas,
                data_troca = DateTime.Now,
                pontos_utilizados = 300,
                id_recompensas = recompensas.id_recompensas,
                id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/TrocasRecompensas/EditTrocasRecompensas/{id_trocas}", EditedTrocasRecompensas);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTrocasRecompensas_ReturnsNoContent_WhenTrocasRecompensasExist()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 300
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var recompensas = new RecompensasModel
            {
                descricao = "Descrição de recompensas",
                pontos_necessarios = -300
            };

            _context.Recompensas.Add(recompensas);
            _context.SaveChanges();

            var trocasRecompensas = new TrocasRecompensasModel
            {
                data_troca = DateTime.Now,
                pontos_utilizados = 300,
                id_recompensas = recompensas.id_recompensas,
                id_usuarios = user.id_usuarios
            };

            _context.TrocasRecompensas.Add(trocasRecompensas);
            _context.SaveChanges();

            //Act
            var response = await _client.DeleteAsync($"/api/TrocasRecompensas/DeleteTrocasRecompensas/{trocasRecompensas.id_trocas}");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTrocasRecompensas_ReturnsNotFound_WhenTrocasRecompensasDoesntExist()
        {
            //Arrange
            var id_trocas = 1234;

            //Act
            var response = await _client.DeleteAsync($"/api/TrocasRecompensas/DeleteTrocasRecompensas/{id_trocas}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
