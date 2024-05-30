namespace Users.HttpModels.Responses;

public class UserInfoHttpResponse
{
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
}