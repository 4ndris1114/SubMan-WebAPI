using SubMan.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SubMan.Services;

public class MongoDBService {
    private readonly IMongoCollection<Subscription> _subscriptionCollection;
    
}