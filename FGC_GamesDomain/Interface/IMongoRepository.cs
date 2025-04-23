using FGC_Games.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGC_Games.Domain.Interface
{
    public interface IMongoRepository
    {
        Task<List<Game>> GetGames();
        Task<Game> GetGamesByIdAsync(int id);
        Task<Game> CreateGamesAsync(Game games);
        Task<Game> UpdateGamesAsync(int id);
        Task<Game> DeleteGameAsync(int id);

    }
}
