namespace Users.Domain.Users.Repos;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddUserAsync(User user, CancellationToken ct = default);
    Task SoftDeleteAsync(Guid id, string adminLogin, CancellationToken ct = default);
    Task ForceDeleteAsync(Guid id, CancellationToken ct = default);
}