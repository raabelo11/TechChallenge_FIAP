using FCG.Domain.DTOs;
using FCG.Domain.Models;

namespace FCG.Application.Interfaces
{
    public interface IUseCaseJogo 
    {
        public Task<ApiResponse> ListarJogos();
        public Task<ApiResponse> Criar(JogoDTO jogos);
    }
}
