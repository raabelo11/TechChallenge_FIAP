using FGC_Games.Domain.Interface;
using FGC_Games.Domain.Models;
using FGC_Games.Infrastructure.Context;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FGC_Games.Infrastructure.Repository.MongoDbRepository
{
    public class RepositoryMongo : IMongoRepository
    {
        private readonly IMongoCollection<Game> _games;
        public RepositoryMongo(ContextMongoDb contextMongoDb)
        {
            _games = contextMongoDb.gamesCollection;

        }
        public Task<Game> CreateGamesAsync(Game games)
        {
            return Task.FromResult(games);
        }

        public Task<Game> DeleteGameAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Game>> GetGames()
        {
            return await _games.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public Task<Game> GetGamesByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdateGamesAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
