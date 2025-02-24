using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Test.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    private readonly ILogger<Controller> _logger;
    private readonly GuapApiService _guapApiService;
    private readonly IConnectionMultiplexer _redisCM;
    
    public Controller(ILogger<Controller> logger, GuapApiService guapApiService, IConnectionMultiplexer redisCM)
    {
        _logger = logger;
        _guapApiService = guapApiService;
        _redisCM = redisCM;
    }

    [HttpPost("add")]
    public void Test1([FromQuery] string key, [FromQuery] string value)
    {
        if (_redisCM.IsConnected == false) throw new Exception("Redis is not connected");
        var db = _redisCM.GetDatabase();
        db.StringSet(key, value);
    }
    
    [HttpGet("get")]
    public IActionResult Test2([FromBody] string key)
    {
        if (_redisCM.IsConnected == false) throw new Exception("Redis is not connected");
        var db = _redisCM.GetDatabase();
        string value = db.StringGet(key);
        if (value == null)
        {
            return BadRequest($"There is no value with the key: {key} ");
        }
        return Ok(value);
    }
}