using SubMan.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SubMan.Services;

public class MongoDBService {
    private readonly IMongoCollection<Subscription> _subscriptionCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _subscriptionCollection = database.GetCollection<Subscription>(mongoDBSettings.Value.CollectionName);
    }
    
}