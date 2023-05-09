using Microsoft.Extensions.Logging;
using Model.ConnectionConfig.Exceptions;
using Model.ConnectionConfig.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson;

namespace Model.ConnectionConfig;

public class MongoDbConnectionService : IMongoDbConnectionService {

    protected readonly string _connectionString;
    protected readonly string? _databaseName;
    protected readonly ILogger<MongoDbConnectionService> _logger;

    public MongoClient DbClient { get; set; }
    public IMongoDatabase Database { get; set; }

    public MongoDbConnectionService(string? connectionString, string? databaseName, ILogger<MongoDbConnectionService>? logger) {
        
        _logger = logger;
        _databaseName = !String.IsNullOrWhiteSpace(connectionString) ? databaseName : "";
        
        if (!String.IsNullOrWhiteSpace(connectionString)) {
            _connectionString = connectionString;
        }
        else {
            throw new MissingConnectionStringException("MongoDbConnectionString is missing");
        }
        
        DbClient = new MongoClient(_connectionString);

        if (!String.IsNullOrWhiteSpace(connectionString)) {
            _connectionString = connectionString;
        }
        else {
            throw new MissingConnectionStringException("Database Name is missing");
        }
        
        Database = DbClient.GetDatabase(_databaseName);


        // var dbList = DbClient.ListDatabases().ToList();
        // _logger.LogInformation("The list of databases on this server is: ");
        // dbList.ForEach(db=>_logger.LogInformation("{Db}", db));
    }
}