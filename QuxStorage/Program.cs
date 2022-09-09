using Microsoft.AspNetCore.Server.Kestrel.Core;
using Utils.Configurations;
using Utils.Configurations.ConfigurationClasses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(x => { x.Limits.MaxRequestBodySize = 1073741824; });

builder.Host.ConfigureAppConfiguration((_, config) => {
    config.AddConfigFiles(new TestConfig());
    config.AddEnvironmentVariables();
}).ConfigureServices((hostConfig, config) => { config.BindConfigFiles(hostConfig.Configuration); });

builder.Services.AddControllers();


var app = builder.Build();

app.MapControllers();
app.Run();