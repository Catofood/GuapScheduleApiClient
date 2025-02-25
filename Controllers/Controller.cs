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

    [HttpPost("Post")]
    public void Test1([FromQuery] string key, [FromQuery] string value)
    {
        using var scope = _scopeFactory.CreateScope();
        var redisCm = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
        if (redisCm.IsConnected == false) throw new Exception("Redis is not connected");
        var db = redisCm.GetDatabase();
        db.StringSet(key, value);
    }
    
    [HttpGet("Get/{key}")]
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

    [HttpPost("ParseFromRedis")]
    public async Task<IActionResult> Test3()
    {
        using var scope = _scopeFactory.CreateScope();
        await _guapApiService.ParseAllStudiesAsync();
        return Ok();
    }
    
    [HttpPost("DownloadToRedis")]
    public async Task<IActionResult> Test4()
    {
        await _guapApiService.DonwloadAllStudiesToRedisAsync();
        return Ok();
    }
    
    [HttpGet("GetRedis")]
    public async Task<IActionResult> Test5()
    {
        return Ok(await _guapApiService.RedisGetAllStudiesAsync());
    }

    [HttpPost("PostRedis")]
    public async Task<IActionResult> Test6([FromBody] string value)
    {
        await _guapApiService.RedisSetAllStudiesAsync(value);
        return Ok();
    }
}