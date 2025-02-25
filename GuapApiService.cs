using System.Text.Json;
using StackExchange.Redis;
using Test.DTO;
using Version = Test.DTO.Version;

namespace Test;

public class GuapApiService(IHttpClientFactory httpClientFactory, IServiceScopeFactory scopeFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private async Task<T> GetDataAsync<T>(string fullEndpointPath)
    {var response = await _httpClient.GetAsync(fullEndpointPath);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Ошибка при запросе данных: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<T>(jsonResponse);

        return data;
    }

    // TODO: Сразу парсим данные при получении и храним уже в своём формате в redis
    // TODO: Сделать вывод данных из redis
    public async Task DonwloadAllStudiesToRedisAsync()
    {
        using var json = await GetDataAsync<JsonDocument>(AllStudyEvents.StudyEvents);
        string textJson = json.RootElement.GetRawText();
        await RedisSetAllStudiesAsync(textJson);
    }

    public async Task RedisSetAllStudiesAsync(string jsonText)
    {
        using var scope = _scopeFactory.CreateScope();
        var redisCm = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
        if (redisCm.IsConnected == false) throw new Exception("Not Connected to redis");
        var db = redisCm.GetDatabase();
        await db.StringSetAsync("sem-events", jsonText);
    }
    public async Task<string> RedisGetAllStudiesAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var redisCm = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
        if (redisCm.IsConnected == false) throw new Exception("Not Connected to redis");
        var db = redisCm.GetDatabase();
        string jsonText = await db.StringGetAsync("sem-events");
        return jsonText;
    }
    
    public async Task<Dictionary<int, WeeklySchedule>> ParseAllStudiesAsync()
    {
        using var doc = JsonDocument.Parse(await RedisGetAllStudiesAsync());
        JsonElement schedulesJson = doc.RootElement.GetProperty("groups");
        var schedules = schedulesJson.Deserialize<Dictionary<int, JsonElement>>();
        var groupNumberToSchedule = new Dictionary<int, WeeklySchedule>();
        foreach (var key in schedules.Keys)
        {
            // Нужно взять по ключу список ивентов на день недели
            var test = schedules[key].GetProperty("monday").Deserialize<List<JsonElement>>();
            Console.WriteLine(test);
            var monday = new List<Event>();
            var tuesday = new List<Event>();
            var wednesday = new List<Event>();
            var thursday = new List<Event>();
            var friday = new List<Event>();
            var saturday = new List<Event>();
            var other = new List<Event>();

        }
        return groupNumberToSchedule ?? throw new InvalidOperationException();
    }
    
    // Пример:
    // {
    //     "groups": {
    //         "1": {
    //             "monday": [
    //             {
    //                 "eventName": "Бюджетный учет и отчетность",
    //                 "eventDateStart": 1739812200,
    //                 "eventDateEnd": 1739817600,
    //                 "roomIds": [
    //                 3
    //                     ],
    //                 "teacherIds": [
    //                 798
    //                     ],
    //                 "departmentId": 3,
    //                 "eventType": "Лекция"
    //             },
    //         }
    //      }
    // }
    
    public async Task<Version> GetVersionAsync()
    {
        return await GetDataAsync<Version>(AllStudyEvents.Version);
    }
}