using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Utils.Configurations;

public static class ConfigurationService
{
    private static readonly List<object> s_configList = new();
    private static string? s_configDirectoryPath;

    private static string GetConfigDirectoryPath()
    {
        if (string.IsNullOrEmpty(s_configDirectoryPath))
        {
            var path = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (path != null && !path.GetDirectories("config").Any())
            {
                Console.WriteLine(path.FullName);
                path = path.Parent;
            }

            Console.ReadKey();
            s_configDirectoryPath = Path.Combine(path?.FullName!, "config");
        }

        return s_configDirectoryPath;
    }
    
    private static string ConfigPath(object config) => 
        Path.Combine(GetConfigDirectoryPath(), GetConfigName(config) + ".json");
    
    private static string GetConfigName(object config) => config.GetType().Name;
    
    private static bool IsConfigExists(object config) => s_configList.Contains(config);

    /// <summary>
    /// Adds JSON configuration files to builder
    /// </summary>
    /// <param name="builder">IConfigurationBuilder</param>
    /// <param name="configs">Array of configuration classes required to connect the module</param>
    /// <returns>IConfigurationBuilder</returns>
    public static IConfigurationBuilder AddConfigFiles(this IConfigurationBuilder builder, params object[] configs) 
    {
        foreach (var config in configs)
        {
            if (IsConfigExists(config)) continue;
            builder.AddJsonFile(ConfigPath(config));
            s_configList.Add(config);
        }

        return builder;
    }

    /// <summary>
    /// Binds configuration classes to the configuration section and adds them to IServiceCollection
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="configuration">IConfiguration</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection BindConfigFiles(this IServiceCollection services, IConfiguration configuration) 
    {
        foreach (var config in s_configList) 
        {
            configuration.Bind(GetConfigName(config), config);
            services.TryAddSingleton(config.GetType(), _ => config);
        }

        return services;
    }
    
}