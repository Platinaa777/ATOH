namespace Users.Domain.Users.Repos;

/// <summary>
/// Более специализированный для поиска репозиторий
/// </summary>
public interface IUserSearchRepository
{
    Task<User?> GetByLogin(string userLogin, CancellationToken ct = default);
    Task<IQueryable<User>> GetActiveUsers(CancellationToken ct = default);
    Task<IQueryable<User>> GetUserWithAgeBiggerThan(int age, CancellationToken ct = default);
}