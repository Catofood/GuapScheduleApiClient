using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Test.Controllers;

[ApiController]
[Route("/api/[controller]")]
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

    [HttpPost("/Test")]
    public void Test()
    {
        if (_redisCM.IsConnected == false) throw new Exception("Redis is not connected");
        var db = _redisCM.GetDatabase();
    }
}