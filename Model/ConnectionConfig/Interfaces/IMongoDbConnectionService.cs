using MongoDB.Driver;

namespace Model.ConnectionConfig.Interfaces; 

public interface IMongoDbConnectionService {
    public MongoClient DbClient { get; set; }
    public IMongoDatabase Database { get; set; }
}