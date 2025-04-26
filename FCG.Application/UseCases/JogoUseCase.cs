using FCG.Application.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.Interface;
using FCG.Domain.Models;

namespace FCG.Application.UseCases
{
    public class JogoUseCase : IUseCaseJogo
    {
        private IJogoRepository _jogoRepository;

        public JogoUseCase(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<ApiResponse> Criar(JogoDTO jogo)
        {
            try
            {
                var game = new Jogos
                {
                    Nome = jogo.Nome,
                    Descricao = jogo.Descricao,
                    Categoria = jogo.Categoria,
                    Preco = jogo.Preco
                };
                return new ApiResponse
                {
                    Ok = await _jogoRepository.AddAsync(game)
                };

            }
            catch(Exception ex)
            {
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
                return new ApiResponse
                {
                    Ok = true,
                    Data = await _jogoRepository.GetAllAsync()
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse
                {
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }
    }
}
