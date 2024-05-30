namespace Users.HttpModels.Requests;

public class ChangeNameRequest
{
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}