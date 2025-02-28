using StackExchange.Redis;
using Test.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InvalidOperationException = System.InvalidOperationException;
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
        var data = JsonConvert.DeserializeObject<T>(jsonResponse);

        return data;
    }

    // TODO: Делаем все нужные запросы на API гуап чтобы получить id групп, зданий, преподавателей и т.п.
    // TODO: Сразу парсим эти данные при получении и храним уже в своём формате в redis, с разделением на группы
    // Что нужно (Запросы):
    // Rooms
    // Buildings
    // Teachers
    // Departments
    //
    public async Task DonwloadAllStudiesToRedisAsync()
    {
        var json = await GetDataAsync<JObject>(Endpoints.StudyEvents);
        string textJson = json.ToString();
        await RedisSetAllStudiesAsync(textJson);
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
        var doc = JObject.Parse(await RedisGetAllStudiesAsync());
        var schedules = JsonConvert.DeserializeObject<Dictionary<int, WeeklySchedule>>(doc["groups"].ToString());
        return schedules ?? throw new NullReferenceException();
    }
    
    
    

    
    public async Task<Version> GetVersionAsync()
    {
        return await GetDataAsync<Version>(Endpoints.Version);
    }
}