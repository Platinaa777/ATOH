using BCrypt.Net;
using Users.Application.Security;

namespace Users.Infrastructure.Security;

public class HasherPassword : IHasherPassword
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string passwordDb, string requestPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(requestPassword, passwordDb, hashType: HashType.SHA384);
    }
}