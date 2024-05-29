namespace Users.Domain.Users.Repos;

public interface IUserRepository
{
    Task<User?> GetById(Guid id, CancellationToken ct = default);
    Task AddUser(User user, CancellationToken ct = default);
    Task UpdateUser(User user, CancellationToken ct = default);
    Task SoftDelete(Guid id, CancellationToken ct = default);
    Task ForceDelete(Guid id, string adminLogin, CancellationToken ct = default);
}