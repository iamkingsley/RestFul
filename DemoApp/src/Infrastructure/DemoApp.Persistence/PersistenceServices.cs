using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Persistence;

public static class PersistenceServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
