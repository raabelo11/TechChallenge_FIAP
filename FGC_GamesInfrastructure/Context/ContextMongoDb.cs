using FGC_Games.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FGC_Games.Infrastructure.Context
{
    public class ContextMongoDb
    {
        public string ConnectionURI { get; set; }
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }

        public IMongoCollection<Game> gamesCollection;

        public ContextMongoDb(IOptions<ContextMongoDb> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionURI);
            IMongoDatabase mongoDatabase = client.GetDatabase(mongoSettings.Value.DatabaseName);
            gamesCollection = mongoDatabase.GetCollection<Game>(mongoSettings.Value.CollectionName);
        }
    }
}