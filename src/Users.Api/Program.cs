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
    .AddCustomAuth(configuration)
    .AddDataLayer(configuration);

var app = builder.Build();

var dockerRunning = app.Configuration["DOCKER_RUNNING"];
if (app.Environment.IsDevelopment() || !string.IsNullOrEmpty(dockerRunning))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseLoggingRequests()
    .UseExceptionHandling()
    .UseIdentityProvider();

app.MapControllers();

app.Run();