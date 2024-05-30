using Microsoft.EntityFrameworkCore;
using Users.DataLayer.Database;
using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Infrastructure.Repos;

public class UserRepository : IUserRepository
{
    private readonly AtonDbContext _dbContext;

    public UserRepository(AtonDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id && x.RevokedOn == null, ct);

    public async Task AddUserAsync(User user, CancellationToken ct = default)
    {
        await _dbContext.Users.AddAsync(user, ct);
    }

    public Task SoftDeleteAsync(Guid id, string adminLogin, CancellationToken ct = default)
    {
        return _dbContext.Users.Where(u => u.Id == id)
            .ExecuteUpdateAsync(entity => entity
                    .SetProperty(f => f.RevokedBy, _ => adminLogin)
                    .SetProperty(field => field.RevokedOn, _ => DateTime.UtcNow), ct);
    }

    public Task ForceDeleteAsync(Guid id, CancellationToken ct = default)
    {
        return _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync(ct);
    }
}