namespace Users.Domain.Exceptions;

public class IntentionException : Exception
{
    public IntentionException()
        : base("This action is permitted for current user")
    {
        
    }
}