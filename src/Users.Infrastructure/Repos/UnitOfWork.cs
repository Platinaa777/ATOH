using Users.DataLayer.Database;
using Users.Domain.Shared;

namespace Users.Infrastructure.Repos;

public class UnitOfWork : IUnitOfWork
{
    private readonly AtohDbContext _dbContext;

    public UnitOfWork(AtohDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}