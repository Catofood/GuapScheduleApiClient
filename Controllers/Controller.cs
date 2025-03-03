using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Application.Static.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    private readonly Client.Client _client;
    private readonly IDatabase _db;

    public Controller(Client.Client client,
        IDatabase db)
    {
        _client = client;
        _db = db;
    }

    // TODO: Позднее удалить
    [HttpPost("Post")]
    public void Test1([FromQuery] string key, [FromQuery] string value)
    {
        _db.StringSet(key, value);
    }

    [HttpGet("Get/{key}")]
    public IActionResult Test2(string key)
    {
        string value = _db.StringGet(key);
        if (value == null) return BadRequest($"There is no value with the key: {key} ");
        return Ok(value);
    }
    
    [HttpGet("GetBuildings")]
    public async Task<IActionResult> GetBuildings()
    {
        return Ok(await _client.GetBuildings());
    }

    [HttpGet("GetRooms")]
    public async Task<IActionResult> GetRooms()
    {
        return Ok(JsonConvert.SerializeObject(await _client.GetRooms()));
    }
    
    // [HttpGet("Get")]
}