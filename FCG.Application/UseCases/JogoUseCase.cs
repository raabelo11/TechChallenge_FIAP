using FCG.Application.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.Interface;
using FCG.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases
{
    public class JogoUseCase : IUseCaseJogo
    {
        private IJogoRepository _jogoRepository;
        private readonly ILogger<JogoUseCase> _logger;

        public JogoUseCase(IJogoRepository jogoRepository, ILogger<JogoUseCase> logger)
        {
            _jogoRepository = jogoRepository;
            _logger = logger;
        }

        public async Task<ApiResponse> AtualizarJogo(Guid guid, int desconto)
        {
            try
            {
                _logger.LogInformation($"Atualizando jogo com ID: {guid} e desconto: {desconto}%");
                var jogo = _jogoRepository.GetByIdAsync(guid).Result;
                if (jogo is null || desconto < 0)
                {
                    _logger.LogWarning($"Jogo com ID: {guid} não encontrado ou desconto inválido: {desconto}%");
                    return new ApiResponse
                    {
                        Errors = ["Não foi possível atualizar esse jogo"]
                    };
                }
                jogo.Preco = jogo.Preco - (jogo.Preco * desconto / 100);
                var result = await _jogoRepository.UpdateAsync(jogo);
                return new ApiResponse
                {
                    Data = jogo,
                    Ok = result
                };
            }

            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar jogo: {ex.Message}, {ex.StackTrace}");
                return new ApiResponse
                {
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

        public async Task<ApiResponse> Criar(JogoDTO jogos)
        {
            try
            {
                _logger.LogInformation($"Criando jogo: Nome: {jogos.Nome}, Descrição: {jogos.Descricao}, Preço: {jogos.Preco}");
                Jogos jogo = new Jogos()
                {
                    Categoria = jogos.Categoria,
                    Descricao = jogos.Descricao,
                    Nome = jogos.Nome,
                    Preco = jogos.Preco
                };

                return new ApiResponse
                {
                    Data = jogo,
                    Ok = await _jogoRepository.AddAsync(jogo),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar jogo: {ex.Message}, {ex.StackTrace}");

                return new ApiResponse
                {
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }


        }

        public async Task<ApiResponse> DeletarJogo(Guid guid)
        {
            try
            {
                _logger.LogInformation($"Deletando jogo com ID: {guid}");
                return new ApiResponse
                {
                    Data = null,
                    Ok = await _jogoRepository.DeleteAsync(guid),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao deletar jogo com ID: {guid}");
                return new ApiResponse
                {
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

        public async Task<ApiResponse> ListarJogos()
        {
            try
            {
                _logger.LogInformation("Listando todos os jogos cadastrados.");
                return new ApiResponse
                {
                    Ok = true,
                    Data = await _jogoRepository.GetAllAsync()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar jogos: {ex.Message}, {ex.StackTrace}");
                return new ApiResponse
                {
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

    }
}
