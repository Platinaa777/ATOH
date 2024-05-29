namespace Users.Domain.Users;

public class User
{
    public Guid Id { get; set; }
    
    public string Login { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public int Gender { get; set; }
    
    public DateTime? Birthday { get; set; }
    /// <summary>
    /// В задании написано `Admin - bool - Указание - является ли пользователь админом`
    /// Но это булевое поле, так что я решил сделать IsAdmin (мне кажется так лучше читается с булевыми полями)
    /// </summary>
    public bool IsAdmin { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime ModifiedOn { get; set; }

    public string ModifiedBy { get; set; } = string.Empty;

    public DateTime RevokedOn { get; set; }

    public string RevokedBy { get; set; } = string.Empty;

    public void RegisterUser(string createdBy, DateTime creationTime)
    {
        CreatedBy = createdBy;
        CreatedOn = creationTime;
    }
}