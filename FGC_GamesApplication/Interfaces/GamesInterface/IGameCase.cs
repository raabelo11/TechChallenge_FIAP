using FGC_Games.Application.UseCase;
using FGC_Games.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGC_Games.Application.Interfaces.GamesInterface
{
    public interface IGameCase
    {
        Task<List<Game>> GetGames();
    }
}
