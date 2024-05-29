namespace Users.Domain.Authorization.Intentions;

public enum AdminIntention
{
    CreateAdmin = 1000,
    DeleteUser = 1001,
    RecoverUser = 1002,
    GetActiveUsers = 2000,
    GetUserForAdmin = 2001,
}