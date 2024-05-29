using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Infrastructure.Repos;

public class UserSearchRepository : IUserSearchRepository
{
    public Task<User?> GetByLogin(string userLogin, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<User>> GetActiveUsers(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<User>> GetUserWithAgeBiggerThan(int age, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}