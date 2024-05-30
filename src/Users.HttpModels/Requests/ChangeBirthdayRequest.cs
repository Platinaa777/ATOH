namespace Users.HttpModels.Requests;

public class ChangeBirthdayRequest
{
    public string Login { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
}