namespace Users.Domain.Users.Repos;

/// <summary>
/// Более специализированный для поиска репозиторий
/// </summary>
public interface IUserSearchRepository
{
    Task<User?> GetByLoginAsync(string userLogin, CancellationToken ct = default);
    Task<IQueryable<User>> GetActiveUsersAsync(CancellationToken ct = default);
    Task<IQueryable<User>> GetUserWithAgeBiggerThanAsync(int age, CancellationToken ct = default);
}