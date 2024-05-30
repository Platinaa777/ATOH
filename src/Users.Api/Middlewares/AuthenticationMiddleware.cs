using Users.Domain.Authentication;

namespace Users.Api.Middlewares;

public class AuthenticationMiddleware
{
    private readonly ILogger<AuthenticationMiddleware> _logger;
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(
        ILogger<AuthenticationMiddleware> logger,
        RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    
    public async Task InvokeAsync(
        HttpContext context,
        IIdentityProvider identityProvider,
        IAuthenticationService authenticationService)
    {
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()
            ?.Replace("Bearer ", string.Empty);

        if (string.IsNullOrWhiteSpace(token))
        {
            _logger.LogInformation("Guest request");
            identityProvider.CurrentIdentity = Identity.CreateGuestIdentity();
            await _next(context);
            return;
        }

        identityProvider.CurrentIdentity = authenticationService
            .AuthenticateIdentity(token);
        _logger.LogInformation("Authenticated user request: {@Identity}", identityProvider.CurrentIdentity);
        await _next(context);
    }
}

public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseIdentityProvider(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}