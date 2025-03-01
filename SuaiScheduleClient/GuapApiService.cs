using System.Globalization;
using Application.DTO;
using Application.Models;
using StackExchange.Redis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DTO_Version = Application.DTO.Version;
using InvalidOperationException = System.InvalidOperationException;
using Version = Application.DTO.Version;

namespace Application.Static;

using Version = DTO_Version;

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
    public async Task<string> GetTeachers()
    {
        return await GetDataAsync<string>(Endpoints.Teachers);
    }

    public async Task<string> GetExamEvents()
    {
        return await GetDataAsync<string>(Endpoints.ExamEvents);
    }
    
    public async Task<string> GetStudyEvents()
    {
        return await GetDataAsync<string>(Endpoints.StudyEvents);
    }

    public async Task<string> GetGroups()
    {
        return await GetDataAsync<string>(Endpoints.Groups);
    }

    public async Task<string> GetDepartments()
    {
        return await GetDataAsync<string>(Endpoints.Departments);
    }

    public async Task<string> GetBuildings()
    {
        return await GetDataAsync<string>(Endpoints.Buildings);
    }
    
    public async Task<string> GetRooms()
    {
        return await GetDataAsync<string>(Endpoints.Rooms);
    }
    
    public async Task<string> GetVersion()
    {
        return await GetDataAsync<string>(Endpoints.Version);
    }
    
    // TODO: Сразу парсим эти данные при получении и храним уже в своём формате в redis, с разделением на группы
    // TODO: Сохраняем изначальную нормализацию, т.к. данных много.
    // TODO: В новой структуре убрать дни недели и рассчитывать их по UNIX времени
    // TODO: Сделать метод для сборки CalendarEvents List
    public async Task DownloadAllStudiesToRedisAsync()
    {
        var json = await GetDataAsync<string>(Endpoints.StudyEvents);
        await RedisSetAllStudiesAsync(json);
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
        var doc = JObject.Parse(await RedisGetAllStudiesAsync());
        var schedules = JsonConvert.DeserializeObject<Dictionary<int, WeeklySchedule>>(doc["groups"].ToString());
        return schedules ?? throw new NullReferenceException();
    }
    
    public async Task<Version> GetVersionAsync()
    {
        return await GetDataAsync<Version>(Endpoints.Version);
    }
}