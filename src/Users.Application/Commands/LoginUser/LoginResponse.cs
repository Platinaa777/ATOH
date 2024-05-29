namespace Users.Application.Commands.LoginUser;

public class LoginResponse
{
    public bool IsSuccess { get; set; }
    public string AccessToken { get; set; }

    public LoginResponse(bool isSuccess, string accessToken)
    {
        IsSuccess = isSuccess;
        AccessToken = accessToken;
    }
}