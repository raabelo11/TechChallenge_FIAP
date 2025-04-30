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

        public async Task<ApiResponse> Criar(JogoDTO jogos)
        {
            try
            {
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
                return new ApiResponse
                {
                    Data = null,
                    Ok = await _jogoRepository.DeleteAsync(guid),
                };
            }
            catch (Exception ex) 
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
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

    }
}
