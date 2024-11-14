using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Persistence.Data;
using DemoApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Persistence;

public static class PersistenceServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options 
            => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
