namespace Users.Api.Filters;

public class SwaggerStartupFilter : IStartupFilter
{
    private readonly IWebHostEnvironment _env;
    
    public SwaggerStartupFilter(IWebHostEnvironment env)
    {
        _env = env;
    }
    
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            if (_env.IsDevelopment() || _env.IsEnvironment("DOCKER_RUNNING"))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            next(app);
        };
    }
}