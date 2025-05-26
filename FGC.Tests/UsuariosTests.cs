using FCG.Application.Interfaces;
using FCG.Controllers;
using FCG.Domain.DTOs;
using FCG.Domain.Enums;
using FCG.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace FGC.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task RetornarListaDeUsuariosValida()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { IdUsuario = Guid.NewGuid(), Nome = "João", Email = "joao@teste.com" }
            };

            var apiResponse = new ApiResponse
            {
                Ok = true,
                Data = usuarios
            };

            var mockService = new Mock<IUseCaseUsuario>();
            mockService.Setup(s => s.List()).ReturnsAsync(apiResponse);
            var controller = new UsuariosController(mockService.Object);

            // Act
            var result = await controller.List();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ApiResponse>(okResult.Value);
            var data = Assert.IsAssignableFrom<IEnumerable<Usuario>>(response.Data);
            Assert.Single(data);
        }

        [Fact]
        public async Task CriarUsuarioComSucesso()
        {
            // Arrange
            var novoUsuario = new UsuarioDTO
            {
                Nome = "Guilherme",
                Email = "guilherme@gmail.com",
                Senha = "12ggfGD@@#34",
                Tipo = TipoUsuario.Administrador
            };

            var context = new ValidationContext(novoUsuario, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(novoUsuario, context, validationResults, true);

            var response = new ApiResponse { Ok = true };

            var mockService = new Mock<IUseCaseUsuario>();
            mockService.Setup(s => s.Add(novoUsuario)).ReturnsAsync(response);

            var controller = new UsuariosController(mockService.Object);

            // Act
            var result = await controller.Add(novoUsuario);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var responseResult = Assert.IsType<ApiResponse>(okResult.Value);

            Assert.True(responseResult.Ok);
            Assert.True(isValid);
        }

        [Fact]
        public void CriarUsuarioComEmailESenhaInvalidos_DeveFalharNaValidacao()
        {
            // Arrange
            var usuarioInvalido = new UsuarioDTO
            {
                Nome = "Lucas",
                Email = "email-invalido",
                Senha = "123",
                Tipo = TipoUsuario.Usuario
            };

            var context = new ValidationContext(usuarioInvalido, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(usuarioInvalido, context, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage!.Contains("E-mail inválido"));
        }

        [Fact]
        public async Task RetornarListaUsuarios_Vazia()
        {
            // Arrange
            var usuarios = new List<Usuario>(); // Lista vazia

            var apiResponse = new ApiResponse
            {
                Ok = true,
                Data = usuarios
            };

            var mockService = new Mock<IUseCaseUsuario>();
            mockService.Setup(s => s.List()).ReturnsAsync(apiResponse);
            var controller = new UsuariosController(mockService.Object);

            // Act
            var result = await controller.List();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ApiResponse>(okResult.Value);
            var data = Assert.IsAssignableFrom<IEnumerable<Usuario>>(response.Data);
            Assert.Empty(data);
        }

        [Fact]
        public async Task AtualizarUsuarioInexistenteValidaçãoDoDataOkEStatusCode()
        {
            // Arrange
            var usuarioInvalido = new UsuarioUpdateDTO
            {
                Id = Guid.NewGuid(),
                Nome = "Nome",
                Email = "email@testeinexistente.com",
                Senha = "Senha123@",
                Tipo = TipoUsuario.Usuario
            };

            var mockService = new Mock<IUseCaseUsuario>();
            mockService
                .Setup(s => s.Update(usuarioInvalido))
                .ReturnsAsync(new ApiResponse
                {
                    Ok = true,
                    Data = "Nenhuma alteração foi realizada."
                });

            var controller = new UsuariosController(mockService.Object);

            // Act
            var result = await controller.Update(usuarioInvalido);

            // Assert
            var statusCode = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ApiResponse>(statusCode.Value);

            Assert.True(response.Ok);
            Assert.Contains("Nenhuma alteração foi realizada.", Convert.ToString(response.Data));
        }

        [Fact]
        public async Task DeletarUsuarioComSucesso()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var response = new ApiResponse
            {
                Ok = true,
            };

            var mockService = new Mock<IUseCaseUsuario>();
            mockService.Setup(s => s.Delete(userId)).ReturnsAsync(response);
            var controller = new UsuariosController(mockService.Object);

            // Act
            var result = await controller.Delete(userId);

            // Assert
            var statusCode = Assert.IsType<OkObjectResult>(result.Result);
            var responseResult = Assert.IsType<ApiResponse>(statusCode.Value);

            Assert.True(responseResult.Ok);
        }
    }
}