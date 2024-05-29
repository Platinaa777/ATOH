namespace Users.Domain.Users.Repos;

/// <summary>
/// Более специализированный для поиска репозиторий
/// </summary>
public interface IUserSearchRepository
{
    Task<User?> GetByLoginAsync(string userLogin, CancellationToken ct = default);
    Task<List<User>> GetActiveUsersAsync(CancellationToken ct = default);
    Task<List<User>> GetUserWithAgeBiggerThanAsync(int age, CancellationToken ct = default);
}