using Microsoft.AspNetCore.Mvc;
using Utils.Configurations.ConfigurationClasses;

namespace QuxStorage.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TestController : Controller
{
    private IServiceProvider _services { get; set; }
    
    public TestController(IServiceProvider services)
    {
        _services = services;
    }

    [HttpGet("Test")]
    public async Task<string> Test()
    {
        return _services.GetService<TestConfig>()!.Test;
    }
}