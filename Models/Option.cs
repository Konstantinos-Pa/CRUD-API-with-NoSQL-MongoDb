using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_Demo.Models
{
    public class Option
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonRequired]
        public string? PollId { get; set; }
        [BsonRequired]
        public string? Text { get; set; }
        public int Votes { get; set; }
    }
}
