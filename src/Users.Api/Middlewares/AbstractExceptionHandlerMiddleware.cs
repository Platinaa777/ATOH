using System.Net;

namespace Users.Api.Middlewares;

public abstract class AbstractExceptionHandlerMiddleware
{
    protected readonly ILogger<AbstractExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
    protected AbstractExceptionHandlerMiddleware(
        ILogger<AbstractExceptionHandlerMiddleware> logger,
        RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    protected abstract (HttpStatusCode code, string message) GetSpecificResponse(Exception exception);

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            
            var (status, message) = GetSpecificResponse(e);
            response.StatusCode = (int)status;
            await response.WriteAsync(message);
        }
    }
}