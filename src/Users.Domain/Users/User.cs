using Users.Domain.Users.Enumerations;

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

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; } = string.Empty;

    public DateTime? RevokedOn { get; set; }

    public string? RevokedBy { get; set; } = string.Empty;

    public void RegisterUser(Guid id, string createdBy, DateTime creationTime, string hashedPassword)
    {
        Id = id; 
        CreatedBy = createdBy;
        CreatedOn = creationTime.ToUniversalTime();
        Password = hashedPassword;
    }

    public void ChangeBirthday(DateTime birthday, string modifiedBy, DateTime modifiedOn)
    {
        Birthday = birthday;
        ModifiedBy = modifiedBy;
        ModifiedOn = modifiedOn.ToUniversalTime();
    }
    
    public void ChangeGender(int gender, string modifiedBy, DateTime modifiedOn)
    {
        Gender = GenderType.FromValue(gender)?.Id ?? throw new ArgumentException("Gender id is invalid");
        ModifiedBy = modifiedBy;
        ModifiedOn = modifiedOn.ToUniversalTime();
    }
    
    public void ChangeName(string name, string modifiedBy, DateTime modifiedOn)
    {
        Name = name;
        ModifiedBy = modifiedBy;
        ModifiedOn = modifiedOn.ToUniversalTime();
    }
    
    public void ChangeLogin(string newLogin, string modifiedBy, DateTime modifiedOn)
    {
        Login = newLogin;
        ModifiedBy = modifiedBy;
        ModifiedOn = modifiedOn.ToUniversalTime();
    }
    
    public void ChangePassword(string newPassword, string modifiedBy, DateTime modifiedOn)
    {
        Password = newPassword;
        ModifiedBy = modifiedBy;
        ModifiedOn = modifiedOn.ToUniversalTime();
    }
}