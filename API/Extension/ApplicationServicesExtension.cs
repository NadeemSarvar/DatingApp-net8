using System;
using API.Data;
using API.Interface;
using API.Servies;
using Microsoft.EntityFrameworkCore;

namespace API.Extension;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenServies, TokenServies>();

        return services;
    }
}
