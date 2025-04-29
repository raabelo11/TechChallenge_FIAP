using FCG.Domain.DTOs;
using FCG.Domain.Models;

namespace FCG.Application.Interfaces
{
    public interface IUseCaseJogo 
    {
        public Task<List<Jogos>> ListarJogos();
        public Task<bool> Criar(Jogos jogos);
    }
}
