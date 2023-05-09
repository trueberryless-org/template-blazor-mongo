using System.Collections.ObjectModel;
using Model.Entities.Authentication;
using Model.Entities.Authentication.Models;

namespace Domain.Repositories.Implementations;

public class UserRepository : ARepository<User>, IUserRepository {

    public UserRepository(IMongoDbConnectionService mongoDbConnectionService) : base(mongoDbConnectionService)
    {
    }

    public async Task<User?> FindByEmailAsync(string email, CancellationToken ct = default)
    {
        var user = await Collection.Find(u => u.Email == email).FirstOrDefaultAsync(cancellationToken: ct);

        return user?.ClearSensitiveData();
    }

    public async Task<User?> AuthorizeAsync(ObjectId id, CancellationToken ct = default) {
        var user = await Collection.Find(u => u.Id == id).FirstOrDefaultAsync(cancellationToken: ct);

        return user?.ClearSensitiveData();
    }

    public Task<User?> AuthorizeAsync(string token, CancellationToken ct = default) {
        throw new NotImplementedException("Bababum");
/*
        var user = await Table
            .Include(u => u.RoleClaims)
            .ThenInclude(rc => rc.Role)
            .AsSplitQuery() // <--- this is the magic
            .FirstOrDefaultAsync(u => false, ct);
        
        return user?.ClearSensitiveData();
*/
    }

    public async Task<User?> AuthorizeAsync(LoginModel model, CancellationToken ct = default) {
        var user = await Collection.Find(u => u.Email == model.Email).FirstOrDefaultAsync(cancellationToken: ct);

        if (user is null) return null;

        if (!User.VerifyPassword(model.Password, user.PasswordHash)) return null!;

        return user.ClearSensitiveData();
    }
    
    public async Task UpdateInfoAsync(User user, CancellationToken ct = default) {
        // check if email is already taken
        var emailExists = await Collection.Find(u => u.Email == user.Email && u.Id == user.Id).AnyAsync(cancellationToken: ct);
        if (emailExists) throw new DuplicateEmailException();

        // update user
        await UpdateAsync(user.Id, user, ct);
    }

}