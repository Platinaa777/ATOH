using Users.Domain.Authentication;

namespace Users.Api.Middlewares;

public class AuthenticationMiddleware
{
    private readonly IIdentityProvider _identityProvider;
    private readonly IAuthenticationService _authenticationService;
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(
        IIdentityProvider identityProvider,
        IAuthenticationService authenticationService, RequestDelegate next)
    {
        _identityProvider = identityProvider;
        _authenticationService = authenticationService;
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()
            ?.Replace("Bearer ", string.Empty);

        if (string.IsNullOrWhiteSpace(token))
        {
            _identityProvider.CurrentIdentity = Identity.CreateGuestIdentity();
            await _next(context);
            return;
        }

        _identityProvider.CurrentIdentity = _authenticationService
            .AuthenticateIdentity(token);

        await _next(context);
    }
}

public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseIdentityProvider(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}