using Users.Api.Extensions;
using Users.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

var configuration = builder.Configuration;

builder.Services
    .AddApplicationServices()
    .AddApiLogging()
    .AddCustomSwagger()
    .AddAuthServices(configuration)
    .AddDataLayer(configuration);

var app = builder.Build();

app
    .UseLoggingRequests()
    .UseExceptionHandling()
    .UseIdentityProvider();

app.MapControllers();

app.Run();