using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Infrastructure.Repos;

public class UserRepository : IUserRepository
{
    public Task<User?> GetById(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task AddUser(User user, CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public Task UpdateUser(User user, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task SoftDelete(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task ForceDelete(Guid id, string adminLogin, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}