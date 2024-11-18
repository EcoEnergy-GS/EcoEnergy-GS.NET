using EcoEnergy_GS.Data;
using EcoEnergy_GS.Models;
using EcoEnergy_GS.Tests.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace EcoEnergy_GS.Tests.Tests
{
    public class UsuarioApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public UsuarioApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        [Fact]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            _context.Usuarios.Add(new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            });

            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync("/api/Usuario/ListarUsuarios");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<UsuarioModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetUserById_ReturnNull_WhenDoesntExist()
        {
            //Arrange
            int id_user = 8989;

            //Act
            var response = await _client.GetAsync($"/api/Usuario/BucarUsuarioPorId/{id_user}");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<UsuarioModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateUser_ReturnsOKUserAndUser()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel",
                senha = "Gabriel123@",
                telefone = "11123456789",
                pontos = 12
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Usuario/CreateUsuario", user);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonUser = await response.Content.ReadFromJsonAsync<ResponseModel<UsuarioModel>>();

            Assert.Equal(user.nome, jsonUser.Dados.nome);
        }

        [Fact]
        public async Task CreateUser_ReturnNull_WhenDoesntCreateUser()
        {
            //Arrange
            var user = new UsuarioModel
            {
                nome = "Gabriel"
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Usuario/CreateUsuario", user);

            //Asserts
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonUser = await response.Content.ReadFromJsonAsync<ResponseModel<UsuarioModel>>();

            Assert.Null(jsonUser.Dados);
        }

        [Fact]
        public async Task EditUser_ReturnsNoContent_WhenUserExist()
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

            var editedUser = new UsuarioModel
            {
                id_usuarios = user.id_usuarios,
                nome = "Juan",
                senha = "Juan123@",
                telefone = "11123456888",
                pontos = 12
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/Usuario/EditUsuario/{user.id_usuarios}", editedUser);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditUser_ReturnsNoContent_WhenUserDoesntExist()
        {
            //Arrange
            int id_user = 1234;

            var editedUser = new UsuarioModel
            {
                id_usuarios = id_user,
                nome = "Juan",
                senha = "Juan123@",
                telefone = "11123456888",
                pontos = 12
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/Usuario/EditUsuario/{id_user}", editedUser);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenUserExist()
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

            //Act
            var response = await _client.DeleteAsync($"/api/Usuario/DeleteUsuario/{user.id_usuarios}");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenUserDoesntExist()
        {
            //Arrange
            var id_user = 1234;

            //Act
            var response = await _client.DeleteAsync($"/api/Usuario/DeleteUsuario/{id_user}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
