using Users.DataLayer.Database;
using Users.Domain.Shared;

namespace Users.Infrastructure.Repos;

public class UnitOfWork : IUnitOfWork
{
    private readonly AtonDbContext _dbContext;

    public UnitOfWork(AtonDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}