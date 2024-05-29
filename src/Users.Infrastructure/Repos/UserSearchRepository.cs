using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Infrastructure.Repos;

public class UserSearchRepository : IUserSearchRepository
{
    public Task<User?> GetByLoginAsync(string userLogin, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<User>> GetActiveUsersAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<User>> GetUserWithAgeBiggerThanAsync(int age, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}