namespace Users.Domain.Authentication;

public class Identity
{
    public string Id { get; }

    /// <summary>
    /// Should fetch from HttpContext in middlewares
    /// </summary>
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public bool IsAdmin { get; set; }

    public static Identity CreateGuestIdentity() => new();

    public Identity()
    {
        UserId = Guid.Empty;
        Login = String.Empty;
        IsAdmin = false;
    }

    public Identity(string id, string login, bool isAdmin)
    {
        Id = id;
        Login = login;
        IsAdmin = isAdmin;
    }
}