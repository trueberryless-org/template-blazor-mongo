

namespace Domain.Repositories.Interfaces;

public interface IRepository<TRootObject> where TRootObject : class {

    Task CreateAsync(TRootObject obj, CancellationToken ct);
    Task CreateRangeAsync(List<TRootObject> objs, CancellationToken ct);
   
    Task<TRootObject?> ReadAsync(ObjectId id, CancellationToken ct);
    Task<List<TRootObject>> ReadAsync(int start, int count, CancellationToken ct);
    Task<List<TRootObject>> ReadAsync(Expression<Func<TRootObject, bool>> filter, CancellationToken ct);
    Task<List<TRootObject>> ReadAllAsync(CancellationToken ct);

    Task UpdateAsync(ObjectId id, TRootObject obj, CancellationToken ct);

    Task DeleteAsync(ObjectId id, TRootObject obj, CancellationToken ct);
   
    Task DeleteRangeAsync(Expression<Func<TRootObject, bool>> filter, CancellationToken ct);
}