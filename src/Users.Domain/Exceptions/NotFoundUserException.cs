namespace Users.Domain.Exceptions;

public class NotFoundUserException : Exception
{
    public NotFoundUserException(string login)
        : base($"User with login: {login} not found")
    {

    }
}