namespace Users.HttpModels.Requests;

public class LoginUserRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}