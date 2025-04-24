using FCG.Application.Interfaces;
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

        public async Task<Jogos> Criar(Jogos jogos)
        {
           var jogoCriado = await _jogoRepository.AddAsync(jogos);
            return jogoCriado;
        }

        public async Task<List<Jogos>> ListarJogos()
        {
            return await _jogoRepository.GetAllAsync();
        }
    }
}
