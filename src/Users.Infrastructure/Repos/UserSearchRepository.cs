using Microsoft.EntityFrameworkCore;
using Users.DataLayer.Database;
using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Infrastructure.Repos;

public class UserSearchRepository : IUserSearchRepository
{
    private readonly AtohDbContext _dbContext;

    public UserSearchRepository(AtohDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<User?> GetByLoginAsync(string userLogin, CancellationToken ct = default)
    {
        return _dbContext.Users
            .FirstOrDefaultAsync(x => 
                x.Login == userLogin && x.RevokedOn == null, ct);
    }

    public Task<List<User>> GetActiveUsersAsync(CancellationToken ct = default)
    {
        return _dbContext.Users
            .AsQueryable()
            .AsNoTracking()
            .Where(x => x.RevokedOn == null)
            .OrderBy(x => x.CreatedOn)
            .ToListAsync(ct);
    }

    public Task<List<User>> GetUserWithAgeBiggerThanAsync(int age, CancellationToken ct = default)
    {
        var minimumBirthday = DateTime.Now.AddYears(-age);

        return _dbContext.Users
            .AsQueryable()
            .AsNoTracking()
            .Where(x => x.Birthday > minimumBirthday)
            .ToListAsync(ct);
    }
}