using Model.Entities.Authentication;

namespace Model.Entities.Log;

[Table("LOG_ENTRIES")]
public class LogEntry
{
    [BsonId(IdGenerator = typeof(ObjectIdGenerator)), BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    [BsonRequired]
    [BsonRepresentation(BsonType.String)]
    [BsonElement("FieldType")]
    public ELogEntryType FieldType { get; set; }
    
    [BsonRequired]
    [BsonDateTimeOptions(DateOnly = true)]
    [BsonElement("DateValue")]
    public DateTime DateValue { get; set; }
    
    [BsonElement("OldValue")]
    public string OldValue { get; set; }
    
    [BsonElement("NewValue")]
    public string NewValue { get; set; }
}