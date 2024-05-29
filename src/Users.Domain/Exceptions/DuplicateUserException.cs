namespace Users.Domain.Exceptions;

public class DuplicateUserException : Exception
{
    public DuplicateUserException(string login)
        : base($"User with login: {login} is already existed in the system")
    {
        
    }
}