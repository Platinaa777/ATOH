using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Users.Application.Authentication;
using Users.Application.Authorization;
using Users.Application.Authorization.Intentions;
using Users.Application.Security;
using Users.DataLayer.AdminOptions;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Infrastructure.Authentication;
using Users.Infrastructure.Security;

namespace Users.Api.Extensions;

public static class AuthManager
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetSection(nameof(AuthOptions)));
        services.AddSingleton<AuthOptions>(sp =>
            sp.GetRequiredService<IOptions<AuthOptions>>().Value);
        
        services
            .AddScoped<IIntentionResolver, AdminIntentionResolver>()
            .AddScoped<IIntentionResolver, UserIntentionResolver>();

        services
            .AddScoped<IIdentityProvider, IdentityProvider>()
            .AddScoped<IIntentionManager, IntentionManager>()
            .AddScoped<IAuthenticationService, JwtAuthenticationService>()
            .AddScoped<IHasherPassword, HasherPassword>();
        
        return services;
    }

    public static IServiceCollection AddCustomAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AdminAccount>(configuration.GetSection(nameof(AdminAccount)));
        services.AddSingleton<AdminAccount>(sp =>
            sp.GetRequiredService<IOptions<AdminAccount>>().Value);
        
        services.AddAuthorization(opts => {
            opts.AddPolicy("OnlyForAdmin", policy => {
                policy.RequireClaim("IsAdmin", "True");
            });
        });
        
        var authOptions = new AuthOptions();
        configuration.GetSection(nameof(AuthOptions)).Bind(authOptions);
        
        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = authOptions.ValidateIssuerKey,
                    ValidateLifetime = authOptions.ValidateLifetime,
                    ValidateAudience = authOptions.ValidateAudience,
                    ValidateIssuer = authOptions.ValidateIssuer,
                    ClockSkew = new TimeSpan(authOptions.ExpireMinutes),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.JwtKey))
                };
            });
        
        return services;
    }
}