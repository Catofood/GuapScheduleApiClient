using System.Text.Json;
using Version = Test.DTO.Version;

namespace Test;

public class GuapApiService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
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
    public async Task<Dictionary<int, JsonElement>> GetAllStudiesAsync()
    {
        var json = await GetDataAsync<JsonDocument>(Endpoints.StudyEvents);
        JsonElement schedulesJson = json.RootElement.GetProperty("groups");
        var schedules = schedulesJson.Deserialize<Dictionary<int, JsonElement>>();
        return schedules ?? throw new InvalidOperationException();
    }
    
    public async Task<Version> GetVersionAsync()
    {
        return await GetDataAsync<Version>(Endpoints.Version);
    }
}