using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_Demo.Models
{
    public class Poll
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? id { get; set; }
        [BsonRequired]
        public string? Question { get; set; }
    }
}
