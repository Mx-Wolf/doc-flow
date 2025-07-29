using DocFlow.Infrastructure.Persistence;
using DocFlow.Infrastructure.Seeding;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocFlow.Infrastructure;
public static class ServiceConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DocFlowDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            o.UseAsyncSeeding(SeedDatabase.SeedAsync);
            o.UseSeeding(SeedDatabase.Seed);
        });
        return services;
    }
}
