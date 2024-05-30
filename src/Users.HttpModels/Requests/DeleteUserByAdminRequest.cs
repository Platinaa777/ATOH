namespace Users.HttpModels.Requests;

public class DeleteUserByAdminRequest
{
    public string Login { get; set; }
    public bool IsSoftDelete { get; set; }
}