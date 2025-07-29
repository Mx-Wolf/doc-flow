using Microsoft.Extensions.DependencyInjection;

namespace DocFlow.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.Scan(scan =>
        {
            scan.FromAssemblies(AssemblyReference.Application)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        return services;
    }
}
