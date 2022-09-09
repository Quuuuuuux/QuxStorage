using DomainLib;
using DomainLib.Entities;
using Microsoft.AspNetCore.Mvc;
using Utils.Configurations.ConfigurationClasses;

namespace QuxStorage.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TestController : Controller
{
    private IServiceProvider _services { get; set; }
    private DatabaseContext _dbContext { get; set; }
    
    public TestController(IServiceProvider services, DatabaseContext dbContext)
    {
        _services = services;
        _dbContext = dbContext;
    }

    [HttpGet("Test")]
    public async Task<string> Test()
    {
        return _services.GetService<TestConfig>()!.Test;
    }

    [HttpPost("TestCreds")]
    public void SaveCredsTest(string login, string password)
    {
        var creds = new Credentials()
        {
            Login = login,
            Password = password
        };
        _dbContext.CredentialsPairs.Add(creds);
        _dbContext.SaveChanges();
    }
}