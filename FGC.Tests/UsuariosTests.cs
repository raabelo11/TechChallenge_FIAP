using Castle.Core.Logging;
using FCG.Application.UseCases;
using FCG.Domain.DTOs;
using FCG.Domain.Enums;
using FCG.Domain.Interface;
using FCG.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FGC.Tests
{
    public class UsuariosTests
    {
        [Fact(DisplayName = "Dado que o e-mail já existe, quando tentar criar um usuário, então deve retornar mensagem de e-mail duplicado")]
        public async Task ValidarCriacaoDeUsuarioComEmailJaExistenteNaBaseDeDados()
        {
            // Arrange
            // Objeto de retorno (mockando retorno do banco de dados para validar a lógica do método).
            var usuario = new Usuario()
            {
                IdUsuario = new Guid(),
                Nome = "Guilherme Magalhães",
                Email = "guilherme2025@gmail.com",
                Senha = "criacaoABDESS123@@",
                Tipo = TipoUsuario.Usuario
            };

            // Objeto de entrada DTO
            var usuarioDTO = new UsuarioDTO()
            {
                Nome = "Guilherme Lima",
                Email = "guilherme2025@gmail.com",
                Senha = "senhaTESTE123@@",
                Tipo = TipoUsuario.Usuario
            };

            var mockLog = new Mock<ILogger<UsuarioUseCase>>();
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(r => r.GetByEmail(usuario.Email)).ReturnsAsync(usuario);

            var useCase = new UsuarioUseCase(mockRepository.Object, mockLog.Object);

            // Act
            var result = await useCase.Add(usuarioDTO);

            // Assert
            Assert.Contains("Email ja existente.", Convert.ToString(result.Data));
            Assert.True(result.Ok);
        }

        [Fact(DisplayName = "Dado que os dados do usuário são válidos, quando criar o usuário, então deve retornar sucesso")]
        public async Task CriarUsuarioComSucesso()
        {
            // Arrange
            // Objeto de entrada DTO
            var usuarioDTO = new UsuarioDTO()
            {
                Nome = "Guilherme Lima",
                Email = "guilherme@gmail.com",
                Senha = "senhaTESTE123@@",
                Tipo = TipoUsuario.Usuario
            };

            // Objeto de entrada banco de dados (Mock)
            var usuario = new Usuario()
            {
                IdUsuario = new Guid(),
                Nome = "Guilherme Magalhães",
                Email = "guilherme2025@gmail.com",
                Senha = "criacaoABDESS123@@",
                Tipo = TipoUsuario.Usuario
            };

            var mockLog = new Mock<ILogger<UsuarioUseCase>>();
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(r => r.GetByEmail(usuarioDTO.Email)).ReturnsAsync(usuario);
            mockRepository.Setup(r => r.AddAsync(It.IsAny<Usuario>())).ReturnsAsync(true);

            var useCase = new UsuarioUseCase(mockRepository.Object, mockLog.Object);

            // Act
            var result = await useCase.Add(usuarioDTO);

            // Assert
            Assert.True(result.Ok);
        }

        [Fact(DisplayName = "Dado que o e-mail e a senha são inválidos, quando validar o usuário, então deve falhar na validação")]
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
            // Valida pelo data Anotations as propriedades preenchidas de UsuarioDTO
            var isValid = Validator.TryValidateObject(usuarioInvalido, context, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage!.Contains("Email invalido."));
        }

        [Fact(DisplayName = "Dado que não há usuários cadastrados, quando listar usuários, então deve retornar uma lista vazia sem erros")]
        public async Task RetornarListaUsuariosVaziaSemErros()
        {
            // Arrange
            var usuarios = new List<Usuario>(); // Lista vazia

            var mockLog = new Mock<ILogger<UsuarioUseCase>>();
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(usuarios);

            var useCase = new UsuarioUseCase(mockRepository.Object, mockLog.Object);

            // Act
            var result = await useCase.List();

            // Assert
            var dataResult = result.Data as ICollection;
            Assert.True(dataResult.Count == 0);
        }

        [Fact(DisplayName = "Dado que o usuário não existe na base, quando tentar atualizar, então deve retornar mensagem indicando nenhuma alteração realizada")]
        public async Task AtualizarUsuarioInexistenteNaBaseDeDados()
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

            var usuarioUpdate = new Usuario
            {
                IdUsuario = Guid.NewGuid(),
                Nome = "Nome",
                Email = "email@testeinexistente.com",
                Senha = "Senha123@",
                Tipo = TipoUsuario.Usuario
            };

            var mockLog = new Mock<ILogger<UsuarioUseCase>>();
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(r => r.UpdateAsync(usuarioUpdate)).ReturnsAsync(null);

            var useCase = new UsuarioUseCase(mockRepository.Object, mockLog.Object);

            // Act
            var result = await useCase.Update(usuarioInvalido);

            // Assert
            Assert.True(result.Ok);
            Assert.Contains("Nenhuma alteracao foi realizada.", Convert.ToString(result.Data));
        }

        [Fact(DisplayName = "Dado que o usuário não existe na base, quando tentar deletar, então deve retornar mensagem de nenhuma alteração realizada")]
        public async Task DeletarUsuarioInexistenteNaBaseDeDados()
        {
            // Arrange
            var guiId = Guid.NewGuid();

            var mockLog = new Mock<ILogger<UsuarioUseCase>>();
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(r => r.DeleteAsync(guiId)).ReturnsAsync(null);

            var useCase = new UsuarioUseCase(mockRepository.Object, mockLog.Object);

            // Act
            var result = await useCase.Delete(guiId);

            // Assert
            Assert.True(result.Ok);
            Assert.Contains("Nenhuma alteracao foi realizada.", Convert.ToString(result.Data));
        }
    }
}