using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Users.Api.Filters;
using Users.Api.Middlewares;
using Users.Application.AssemblyInfo;
using Users.Application.Authentication;
using Users.Application.Authorization;
using Users.Application.Authorization.Intentions;
using Users.Application.Behavior;
using Users.Application.Commands.RegisterUser;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Users.Repos;
using Users.Infrastructure.Authentication;
using Users.Infrastructure.Repos;

namespace Users.Api.Extensions;

public static class ServiceManager
{
    public static IServiceCollection AddApiLogging(this IServiceCollection services)
    {
        services.AddLogging(b => b.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .WriteTo.Console()
            .CreateLogger()));

        return services;
    }
    
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<RegisterUser>();
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(
            ApplicationAssembly.Assembly,
            includeInternalTypes: true);

        services.AddAutoMapper(config => 
            config.AddMaps(
        Assembly.GetEntryAssembly(),
                ApplicationAssembly.Assembly));

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserSearchRepository, UserSearchRepository>();
        
        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        
        services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

        return services;
    }

    public static IServiceCollection AddAuthServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));
        services.AddSingleton<AuthOptions>(sp =>
            sp.GetRequiredService<IOptions<AuthOptions>>().Value);
        
        services
            .AddScoped<IIntentionResolver, AdminIntentionResolver>()
            .AddScoped<IIntentionResolver, UserIntentionResolver>();

        services
            .AddScoped<IIdentityProvider, IdentityProvider>()
            .AddScoped<IIntentionManager, IntentionManager>()
            .AddScoped<IAuthenticationService, JwtAuthenticationService>();

        return services;
    }
}