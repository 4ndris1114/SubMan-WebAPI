using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace SubMan.Models;

public class Subscription {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set;}
    public string Name { get; set; } = null!;
    public string? Comment { get; set; }
    public float Price { get; set; }
    public string? Currency { get; set; }
    public int Interval { get; set; } // in days
}
