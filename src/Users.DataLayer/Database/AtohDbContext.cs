using Microsoft.EntityFrameworkCore;
using Users.DataLayer.Configurations;
using Users.Domain.Users;

namespace Users.DataLayer.Database;

public class AtohDbContext : DbContext
{
    public AtohDbContext(DbContextOptions<AtohDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(builder);
    }
}