using Application.Controllers.DTO;
using StackExchange.Redis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application.Client;


public class Client(
    IHttpClientFactory httpClientFactory,
    IServiceScopeFactory scopeFactory,
    Endpoints endpoints,
    HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;
    
    private async Task<T> GetDataAsync<T>(string fullEndpointPath)
    {
        var response = await _httpClient.GetAsync(fullEndpointPath);
        if (!response.IsSuccessStatusCode) throw new Exception($"Ошибка при запросе данных: {response.StatusCode}");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<T>(jsonResponse);
        return data;
    }

    public async Task<string> GetTeachers()
    {
        return await GetDataAsync<string>(endpoints.Teachers);
    }

    public async Task<string> GetExamEvents()
    {
        return await GetDataAsync<string>(endpoints.ExamEvents);
    }

    public async Task<string> GetStudyEvents()
    {
        return await GetDataAsync<string>(endpoints.StudyEvents);
    }

    public async Task<string> GetGroups()
    {
        return await GetDataAsync<string>(endpoints.Groups);
    }

    public async Task<string> GetDepartments()
    {
        return await GetDataAsync<string>(endpoints.Departments);
    }

    public async Task<string> GetBuildings()
    {
        return await GetDataAsync<string>(endpoints.Buildings);
    }

    public async Task<string> GetRooms()
    {
        return await GetDataAsync<string>(endpoints.Rooms);
    }

    public async Task<string> GetVersion()
    {
        return await GetDataAsync<string>(endpoints.Version);
    }

    // TODO: Сразу парсим эти данные при получении и храним уже в своём формате в redis, с разделением на группы
    // TODO: Сохраняем изначальную нормализацию, т.к. данных много.
    // TODO: В новой структуре убрать дни недели и рассчитывать их по UNIX времени

}