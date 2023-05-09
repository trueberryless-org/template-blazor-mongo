using Model.Entities.Log;

namespace Model.Entities.Authentication;

[Table("USERS")]
public class User {
    [BsonId(IdGenerator = typeof(ObjectIdGenerator)), BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonRequired]
    [BsonElement("Username")]
    public string Username { get; set; } = null!;

    [BsonRequired]
    [BsonElement("Email")]
    // UNIQUE
    public string Email { get; set; } = null!;

    public bool ShouldSerializeAge()
    {
        var client = new MongoClient("mongodb://root:toor@localhost:27017/?authMechanism=DEFAULT");
        var database = client.GetDatabase("db");
        var collection = database.GetCollection<User>("User");
        
        /*var cmdStr = "{ createIndexes: 'User', indexes: [ { key: { Email: 1 }, name: 'email-uniq-1', unique: true } ] }";
        var cmd = BsonDocument.Parse(cmdStr);
        var result = database.RunCommand<BsonDocument>(cmd);*/

        return collection.CountDocuments(u => u.Email == Email) > 0;
    }

    [BsonRequired]
    [BsonElement("PasswordHash")]
    public string PasswordHash { get; set; } = null!;

    [BsonIgnore]
    public string LoginPassword { get; set; } = null!;
    
    [BsonRepresentation(BsonType.Array)]
    [BsonElement("Roles")]
    public List<Role> Roles { get; set; }
    
    [BsonRepresentation(BsonType.Array)]
    [BsonElement("LogEntries")]
    public List<LogEntry> LogEntries { get; set; }
    
    public User ClearSensitiveData() {
        // PasswordHash = null!;
        return this;
    }

    public static string HashPassword(string plainPassword) {
        var salt = BC.GenerateSalt(8);
        return BC.HashPassword(plainPassword, salt);
    }

    public static bool VerifyPassword(string plainPassword, string hashedPassword) {
        return BC.Verify(plainPassword, hashedPassword);
    }

    public User Clone()
    {
        return new User()
        {
            Id = this.Id,
            Username = this.Username,
            Email = this.Email
        };
    }
}