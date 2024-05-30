using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Users.Application.AssemblyInfo;
using Users.Application.Behavior;
using Users.Application.Commands.RegisterUser;
using Users.DataLayer.Database;
using Users.Domain.Shared;
using Users.Domain.Users.Repos;
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
        
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo() { 
                Title = $"{Assembly.GetEntryAssembly()!.GetName().Name}",
                Version = "v1" 
            });
            opt.CustomSchemaIds(x => x.FullName);

            // Add JWT Authentication options
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    securityScheme,
                    new string[] { }
                }
            });
        }); 

        return services;
    }

    

    public static IServiceCollection AddDataLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AtonDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // думаю миграции не сильно нужны для тестового задания, поэтому воспользуемся EnsureCreated()
        using var serviceProvider = services.BuildServiceProvider();
        using var context = serviceProvider.GetRequiredService<AtonDbContext>();
        context.Database.EnsureCreated();

        return services;
    }
}