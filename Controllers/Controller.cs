using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Test.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    private readonly ILogger<Controller> _logger;
    private readonly GuapApiService _guapApiService;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public Controller(ILogger<Controller> logger, GuapApiService guapApiService, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _guapApiService = guapApiService;
        _scopeFactory = scopeFactory;
    }

    [HttpPost("post")]
    public void Test1([FromQuery] string key, [FromQuery] string value)
    {
        using var scope = _scopeFactory.CreateScope();
        var redisCm = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
        if (redisCm.IsConnected == false) throw new Exception("Redis is not connected");
        var db = redisCm.GetDatabase();
        db.StringSet(key, value);
    }
    
    [HttpGet("get/{key}")]
    public IActionResult Test2(string key)
    {
        using var scope = _scopeFactory.CreateScope();
        var redisCm = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
        if (redisCm.IsConnected == false) throw new Exception("Redis is not connected");
        var db = redisCm.GetDatabase();
        string value = db.StringGet(key);
        if (value == null)
        {
            return BadRequest($"There is no value with the key: {key} ");
        }
        return Ok(value);
    }
}