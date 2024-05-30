using System.Net;
using Newtonsoft.Json;
using Users.Domain.Exceptions;
using ValidationException = FluentValidation.ValidationException;

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
            case InvalidAuthTokenException 
                or DuplicateUserException
                or ArgumentException:
                code = HttpStatusCode.BadRequest;
                break;
            case IntentionException:
                code = HttpStatusCode.Unauthorized;
                break;
            case NotFoundUserException:
                code = HttpStatusCode.NotFound;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                _logger.LogError("Exception: {@Exception}", exception);
                errorMessage = "Some server error. Please try later";
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