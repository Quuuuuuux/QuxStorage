using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utils.Configurations.ConfigurationClasses;

namespace DomainLib;

public static class DomainLib
{
    public static IServiceCollection AddDomainLib(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>( b => b
            .UseNpgsql(configuration.GetConnectionString("Database"),
                x => x.MigrationsAssembly(nameof(DomainLib)))
            //.UseApplicationServiceProvider(services.BuildServiceProvider())
            .UseSnakeCaseNamingConvention());
        
        return services;
    }
}