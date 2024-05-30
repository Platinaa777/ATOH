namespace Users.HttpModels.Requests;

public class ChangePasswordRequest
{
    public string CurrentLogin { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}