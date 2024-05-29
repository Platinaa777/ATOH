namespace Users.Application.Security;

public interface IHasherPassword
{
    string HashPassword(string password);
    bool Verify(string passwordDb, string requestPassword);
}