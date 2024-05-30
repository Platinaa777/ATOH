namespace Users.HttpModels.Requests;

public class ChangeLoginRequest
{
    public string CurrentLogin { get; set; } = string.Empty;
    public string NewLogin { get; set; } = string.Empty;
}