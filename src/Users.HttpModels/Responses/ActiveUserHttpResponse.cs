namespace Users.HttpModels.Responses;

/// <summary>
/// В задании ничего не сказано какие поля возвращать, поэтому верну все
/// Ну это все равно админский эндпоинт, так что и не слишком критично
/// </summary>
public class ActiveUserHttpResponse
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; } = string.Empty;
    public DateTime? RevokedOn { get; set; }
    public string? RevokedBy { get; set; } = string.Empty;
}