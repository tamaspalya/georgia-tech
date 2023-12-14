using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WarehouseAPI.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public string? Genre { get; set; }
        public required string Language { get; set; }
        public string? Description { get; set; }
        public required string SellerId { get; set; }
        public int Quantity { get; set; }
    }
}
