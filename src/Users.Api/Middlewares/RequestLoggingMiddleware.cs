namespace Users.Api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(
        ILogger<RequestLoggingMiddleware> logger,
        RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = context.TraceIdentifier;
        
        _logger.LogInformation("Request with trace id: {@TraceId} to endpoint: {@RequestPath}",
            traceId, context.Request.Path.Value);

        await _next(context);
        
        _logger.LogInformation("Request with trace id: {@TraceId} to endpoint: {@RequestPath} with status code: {@StatusCode}",
            traceId, context.Request.Path.Value, context.Response.StatusCode);
    }
}

public static class RequestMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingRequests(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}