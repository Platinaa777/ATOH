namespace Users.Application.Queries.GetUserInfo;

public class GetUserResponse
{
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
}