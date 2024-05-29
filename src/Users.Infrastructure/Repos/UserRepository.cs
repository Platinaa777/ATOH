using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Infrastructure.Repos;

public class UserRepository : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task AddUserAsync(User user, CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public Task UpdateUserAsync(User user, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task ForceDeleteAsync(Guid id, string adminLogin, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}