namespace Users.HttpModels.Requests;

public class RegisterRequest
{
    public required string Login { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    public required int Gender { get; set; }
    public required DateTime Birthday { get; set; }
    public required bool ShouldBeAdmin { get; set; }
}