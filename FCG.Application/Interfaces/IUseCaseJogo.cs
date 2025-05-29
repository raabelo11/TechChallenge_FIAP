using FCG.Domain.DTOs;
using FCG.Domain.Models;

namespace FCG.Application.Interfaces
{
    public interface IUseCaseJogo 
    {
        public Task<ApiResponse> ListarJogos();
        public Task<ApiResponse> Criar(JogoDTO jogos);
        public Task<ApiResponse> DeletarJogo(Guid guid);
        public Task<ApiResponse> AtualizarJogo(Guid guid,int desconto);
        public Task<ApiResponse> ListarCategorias();
    }
}
