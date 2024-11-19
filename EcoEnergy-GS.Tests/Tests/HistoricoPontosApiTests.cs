using EcoEnergy_GS.Data;
using EcoEnergy_GS.DTO.Usuarios;
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
    public class HistoricoPontosApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public HistoricoPontosApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        [Fact]
        public async Task GetHistoricoPontos_ReturnsListOfHistoricoPontos()
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

            var historico = new HistoricoPontosModel
            {
                data_historico = DateTime.Now,
                quantidade = 10,
                id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.GetAsync("/api/HistoricoPontos/ListarHistorico");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<HistoricoPontosModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetByIdHistoricoPontos_ReturnHistoricoPontos()
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

            var historico = new HistoricoPontosModel
            {
                data_historico = DateTime.Now,
                quantidade = 10,
                id_usuarios = user.id_usuarios
            };

            _context.HistoricoPontos.Add(historico);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync($"/api/HistoricoPontos/BucarHistoricoPorId/{historico.id_historico}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<HistoricoPontosModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetHistoricoPontosById_ReturnNull_WhenDoesntExist()
        {
            //Arrange
            int id_historico = 8989;

            //Act
            var response = await _client.GetAsync($"/api/HistoricoPontos/BucarHistoricoPorId/{id_historico}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<HistoricoPontosModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateHistoricoPontos_ReturnsOKUserAndHistoricoPontos()
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

            var historico = new HistoricoPontosModel
            {
                data_historico = DateTime.Now,
                quantidade = 10,
                id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/HistoricoPontos/CreateHistorico", historico);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<HistoricoPontosModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(historico.data_historico, json.Dados.data_historico);
        }

        //[Fact]
        //public async Task CreateHistoricoPontos_ReturnNull_WhenNotEnoughtData()
        //{
        //    //Arrange
        //    var historico = new HistoricoPontosModel
        //    {
        //        quantidade = 1
        //    };

        //    //Act
        //    var response = await _client.PostAsJsonAsync("/api/HistoricoPontos/CreateHistorico", historico);

        //    //Asserts
        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}

        [Fact]
        public async Task EditHistoricoPontos_ReturnsNoContent_WhenHistoricoPontosExist()
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

            var historico = new HistoricoPontosModel
            {
                data_historico = DateTime.Now,
                quantidade = 10,
                id_usuarios = user.id_usuarios
            };

            _context.HistoricoPontos.Add(historico);
            _context.SaveChanges();

            var editedHistorico = new HistoricoPontosModel
            {
                id_historico = historico.id_historico,
                data_historico = DateTime.Now,
                quantidade = 14,
                id_usuarios = user.id_usuarios
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/HistoricoPontos/EditHistorico/{historico.id_historico}", editedHistorico);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditHistoricoPontos_ReturnsNotFound_WhenHistoricoPontosDoesntExist()
        {
            //Arrange
            int id_historico = 1234;

            var editedHistorico = new HistoricoPontosModel
            {
                id_historico = id_historico,
                data_historico = DateTime.Now,
                quantidade = 10,
                id_usuarios = 1
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/HistoricoPontos/EditHistorico/{id_historico}", editedHistorico);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteHistoricoPontos_ReturnsNoContent_WhenHistoricoPontosExist()
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

            var historico = new HistoricoPontosModel
            {
                data_historico = DateTime.Now,
                quantidade = 10,
                id_usuarios = user.id_usuarios
            };

            _context.HistoricoPontos.Add(historico);
            _context.SaveChanges();

            //Act
            var response = await _client.DeleteAsync($"/api/HistoricoPontos/DeleteHistorico/{historico.id_historico}");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteHistoricoPontos_ReturnsNoContent_WhenHistoricoPontosDoesntExist()
        {
            //Arrange
            var id_historico = 1234;

            //Act
            var response = await _client.DeleteAsync($"/api/HistoricoPontos/DeleteHistorico/{id_historico}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
