using System.ComponentModel.DataAnnotations;
using FCG.Application.Interfaces;
using FCG.Application.UseCases;
using FCG.Controllers;
using FCG.Domain.DTOs;
using FCG.Domain.Interface;
using FCG.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FGC.Tests
{
    public class JogosTests
    {
        [Fact(DisplayName = "Ao chamar o endpoint de cadastrar jogo devemos cadastrar um jogo com dados válidos")]
        public async Task CadastrarJogo_Valido()
        {
            var novoJogo = new JogoDTO
            {
                Nome = "Novo Jogo",
                Descricao = "Descrição do novo jogo",
                Preco = 59.99m,
            };
            var context = new ValidationContext(novoJogo, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(novoJogo, context, validationResults, true);

            var response = new ApiResponse
            {
                Ok = true,
                Data = novoJogo
            };

            var mockService = new Mock<IUseCaseJogo>();
            mockService.Setup(s => s.Criar(novoJogo)).ReturnsAsync(response);
            var controller = new JogosController(mockService.Object);

            //Act
            var result = await controller.Create(novoJogo);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.True(apiResponse.Ok);
            Assert.Equal(novoJogo, apiResponse.Data);
        }

        [Fact(DisplayName = "Ao chamar o endpoint de listar jogos, devemos retornar uma lista de jogos cadastrados")]
        public async Task ListarJogos_Valido()
        {
            // Arrange
            var jogos = new List<JogoDTO>
            {
                new JogoDTO { Nome = "Jogo 1", Descricao = "Descrição do Jogo 1", Preco = 49.99m },
                new JogoDTO { Nome = "Jogo 2", Descricao = "Descrição do Jogo 2", Preco = 59.99m }
            };
            var response = new ApiResponse
            {
                Ok = true,
                Data = jogos
            };
            var mockService = new Mock<IUseCaseJogo>();
            mockService.Setup(s => s.ListarJogos()).ReturnsAsync(response);
            var controller = new JogosController(mockService.Object);
            // Act
            var result = await controller.Get();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.True(apiResponse.Ok);
            Assert.Equal(jogos, apiResponse.Data);
        }
        [Fact(DisplayName = "Ao chamar o endpoint de deletar jogo, devemos remover o jogo com sucesso passando o id do jogo")]
        public async Task DeletarJogo_Valido()
        {
            // Arrange
            var jogoId = Guid.NewGuid();
            var response = new ApiResponse
            {
                Ok = true,
            };
            var mockService = new Mock<IUseCaseJogo>();
            mockService.Setup(s => s.DeletarJogo(jogoId)).ReturnsAsync(response);
            var controller = new JogosController(mockService.Object);
            // Act
            var result = await controller.Delete(jogoId);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.True(apiResponse.Ok);
        }
        [Fact(DisplayName = "Ao chamar o endpoint de atualizar jogo, devemos aplicar um desconto no jogo com sucesso passando o id do jogo e o valor do desconto")]
        public async Task AtualizarJogo_Valido()
        {
            // Arrange
            var jogoId = new Guid("BDC6DB83-4C0A-4E1A-ACB8-DAB816E36CD5");
            int desconto = -10;

            var jogo = new Jogos
            {
                IdJogo = jogoId,
                Nome = "Senhor dos, aneis",
                Preco = 100,
                Descricao = "eteste",
                Categoria = FCG.Domain.Enums.Generos.Aventura
            };

            var mockRepository = new Mock<IJogoRepository>();

            // Mocka o retorno do banco de dados
            mockRepository.Setup(r => r.GetByIdAsync(jogoId)).ReturnsAsync(jogo);
            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Jogos>())).ReturnsAsync(true);

            var useCase = new JogoUseCase(mockRepository.Object);

            // Act
            // Valida a logica, como se tivesse o retorno do banco de dados
            var result = await useCase.AtualizarJogo(jogoId, desconto);

            // Assert
            // Valida o que vc quiser
            Assert.False(result.Ok);
            Assert.Contains("Não foi possível atualizar esse jogo", result.Errors);
        }
    }
}
