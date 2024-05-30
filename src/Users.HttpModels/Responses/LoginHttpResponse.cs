namespace Users.HttpModels.Responses;

public class LoginHttpResponse
{
    public bool IsSuccess { get; set; }
    public string AccessToken { get; set; }
    public string? Error { get; set; }
}