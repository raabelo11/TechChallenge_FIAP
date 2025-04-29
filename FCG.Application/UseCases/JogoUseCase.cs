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

        public async Task<bool> Criar(Jogos jogos)
        {
           var jogoCriado = await _jogoRepository.AddAsync(jogos);
            return jogoCriado;
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
