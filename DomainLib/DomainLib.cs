using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utils.Configurations.ConfigurationClasses;

namespace DomainLib;

public static class DomainLib
{
    public static IServiceCollection AddDomainLib(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(b => b
            .UseNpgsql(services.BuildServiceProvider().GetService<DatabaseConfig>()!.ConnectionString,
                x => x.MigrationsAssembly(nameof(DomainLib)))
            //.UseApplicationServiceProvider(services.BuildServiceProvider())
            .UseSnakeCaseNamingConvention());
        
        return services;
    }
}