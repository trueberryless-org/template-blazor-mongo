namespace Domain.Repositories.Implementations;

public class ARepository<TRootObject> : IRepository<TRootObject> where TRootObject : class {

    private readonly IMongoDbConnectionService _mongoDbConnectionService;
    public IMongoDatabase? Database { get; set; }
    public IMongoCollection<TRootObject> Collection { get; set; }
    
    public ARepository(IMongoDbConnectionService mongoDbConnectionService) {
        _mongoDbConnectionService = mongoDbConnectionService;
        Database = _mongoDbConnectionService.Database;
        
        Collection = Database.GetCollection<TRootObject>(typeof(TRootObject).Name.ToLower());
    }
    
    public async Task CreateAsync(TRootObject obj, CancellationToken ct) {
        await Collection.InsertOneAsync(obj, cancellationToken: ct);
    }

    public async Task CreateRangeAsync(List<TRootObject> objs, CancellationToken ct) {
        await Collection.InsertManyAsync(objs, cancellationToken: ct);
    }

    public async Task<TRootObject?> ReadAsync(ObjectId id, CancellationToken ct) {
        var filter = Builders<TRootObject>.Filter.Eq("_id", id);
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken: ct);
    }

    public async Task<List<TRootObject>> ReadAsync(int start, int count, CancellationToken ct) 
        => await Collection.Find(s => true).Skip(start).Limit(count).ToListAsync(cancellationToken: ct);

    public async Task<List<TRootObject>> ReadAsync(Expression<Func<TRootObject, bool>> filter, CancellationToken ct) 
       =>  await Collection.Find(filter).ToListAsync(cancellationToken: ct);
   

    public async Task<List<TRootObject>> ReadAllAsync(CancellationToken ct) {
        var queryableCollection = Collection.AsQueryable();
        return await queryableCollection.ToListAsync(cancellationToken: ct);
    }

    public async Task UpdateAsync(ObjectId id, TRootObject obj, CancellationToken ct) {
        var filter = Builders<TRootObject>.Filter.Eq("_id", id);
        var old = await Collection.Find(filter).FirstAsync(cancellationToken: ct);

        if (old is null) return;
        
        await Collection.ReplaceOneAsync(filter, obj, cancellationToken: ct);
    }
    
    public async Task DeleteAsync(ObjectId id, TRootObject obj, CancellationToken ct) {
        var filter = Builders<TRootObject>.Filter.Eq("_id", id);
        var old = await Collection.Find(filter).FirstAsync(cancellationToken: ct);
        if (old is null) return;
        
        await Collection.DeleteOneAsync(filter, ct);
    }

    public async Task DeleteRangeAsync(Expression<Func<TRootObject, bool>> filter, CancellationToken ct) {
        await Collection.DeleteManyAsync(filter, ct);
    }
}