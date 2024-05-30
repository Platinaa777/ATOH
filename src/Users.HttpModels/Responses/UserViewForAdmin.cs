namespace Users.HttpModels.Responses;

public class UserViewForAdmin
{
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
    public DateTime? RevokedOn { get; set; }
}