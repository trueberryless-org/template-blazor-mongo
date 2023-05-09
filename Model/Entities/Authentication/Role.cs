namespace Model.Entities.Authentication;

[Table("ROLES")]
public class Role {
    [BsonId(IdGenerator = typeof(ObjectIdGenerator)), BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    [BsonElement("Identifier")]
    // UNIQUE
    public string Identifier { get; set; } = null!;
    
    [BsonIgnoreIfNull]
    [BsonElement("Description")]
    public string? Description { get; set; }
    
    // Insert Default Data
}