using FGC_Games.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace FGC_Games.Domain.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string gameName {  get; set; }
        public decimal Price {  get; set; }
        [BsonRepresentation(BsonType.String)]
        public categoryGames categoryGames { get; set; }
    }
}
