namespace Users.Application.Commands.LoginUser;

public class LoginResponse
{
    public bool IsSuccess { get; set; }
    public string AccessToken { get; set; }
    public string? Error { get; set; }

    public LoginResponse(bool isSuccess, string accessToken, string? error = null)
    {
        IsSuccess = isSuccess;
        AccessToken = accessToken;
        Error = error;
    }
}