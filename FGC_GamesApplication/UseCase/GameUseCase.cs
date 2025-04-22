using FGC_Games.Application.Interfaces.GamesInterface;
using FGC_Games.Domain.Interface;
using FGC_Games.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGC_Games.Application.UseCase
{
    public class GameUseCase : IGameCase
    {
        private readonly IGame _game;
        public GameUseCase(IGame game)
        {
            _game = game;
        }
        public Task<List<Game>> GetGames()
        {
            return _game.GetGames();
        }
    }
}
