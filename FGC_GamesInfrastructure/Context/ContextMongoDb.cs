using FGC_Games.Domain.Models;
using FGC_Games.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FGC_Games.Infrastructure.Context
{
    public class ContextMongoDb
    {
        private readonly IMongoDatabase _database;

        public ContextMongoDb(IOptions<MongoDbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<Game> gamesCollection
        {
            get
            {
                return _database.GetCollection<Game>("games");
            }
        }
    }
}