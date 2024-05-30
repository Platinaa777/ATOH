using Microsoft.EntityFrameworkCore;
using Users.DataLayer.AdminOptions;
using Users.DataLayer.Configurations;
using Users.Domain.Users;

namespace Users.DataLayer.Database;

public class AtonDbContext : DbContext
{
    private readonly AdminAccount _adminAccount;

    public AtonDbContext(
        DbContextOptions<AtonDbContext> options,
        AdminAccount adminAccount) : base(options)
    {
        _adminAccount = adminAccount;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.Entity<User>().HasData(new User
        {
                Id = Guid.Parse(_adminAccount.Id),
                Name = _adminAccount.Name,
                Login = _adminAccount.Login,
                Password = _adminAccount.Password,
                Gender = _adminAccount.Gender,
                IsAdmin = _adminAccount.IsAdmin,
                CreatedBy = _adminAccount.CreatedBy,
                CreatedOn = _adminAccount.CreatedOn.ToUniversalTime()
            });
    }
}