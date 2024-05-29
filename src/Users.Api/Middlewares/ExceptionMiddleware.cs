using System.ComponentModel.DataAnnotations;
using System.Net;
using Newtonsoft.Json;

namespace Users.Api.Middlewares;

public class ExceptionMiddleware : AbstractExceptionHandlerMiddleware
{
    public ExceptionMiddleware(
        ILogger<ExceptionMiddleware> logger,
        RequestDelegate next) : base(logger, next)
    {
    }

    protected override (HttpStatusCode code, string message) GetSpecificResponse(Exception exception)
    {
        HttpStatusCode code;
        string errorMessage = exception.Message;
        switch (exception)
        {
            case ValidationException:
                code = HttpStatusCode.BadRequest;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                errorMessage = exception.Message;
                break;
        }
        return (code, JsonConvert.SerializeObject(new {errorMessage}));
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}