namespace Users.HttpModels.Requests;

public class ChangeGenderRequest
{
    public string Login { get; set; } = string.Empty;
    public int Gender { get; set; }
}