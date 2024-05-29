namespace Users.Domain.Exceptions;

public class InvalidAuthTokenException : Exception
{
    public InvalidAuthTokenException()
        : base("Invalid auth token")
    {
        
    }
}