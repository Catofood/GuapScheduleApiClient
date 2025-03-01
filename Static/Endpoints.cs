using System.Text;

namespace Application.Static;

// Предназначение класса: Возвращать эндпоинты расписания ГУАП
public class Endpoints
{
    private readonly string DirectoryName = "Endpoints";
    private readonly IConfiguration _config;
    public Endpoints(IConfiguration config)
    {
        _config = config;
    }

    private string GetPath(string? endpoint)
    {
        var route = _config[$"{DirectoryName}:{endpoint}"];
        if (string.IsNullOrEmpty(endpoint))
        {
            throw new Exception($"Failed to retrieve the API URL from the configuration. Make sure the '{DirectoryName}:{endpoint}' key is present in appsettings.json.");
        }
        return GetBaseApiUrl() + route;
    }

    private string GetBaseApiUrl()
    { 
        var url = _config[$"{DirectoryName}:Url"];
        if (string.IsNullOrEmpty(url))
        {
            throw new Exception($"Failed to retrieve the API URL from the configuration. Make sure the '{DirectoryName}:Url' key is present in appsettings.json.");
        }
        return url;
    }
    public string Version => GetPath(_config[$"{DirectoryName}:Version"]);
    public string Rooms => GetPath(_config[$"{DirectoryName}:Rooms"]);
    public string Buildings => GetPath(_config[$"{DirectoryName}:Buildings"]);
    public string Departments => GetPath(_config[$"{DirectoryName}:Departments"]);
    public string Teachers => GetPath(_config[$"{DirectoryName}:Teachers"]);
    public string Groups => GetPath(_config[$"{DirectoryName}:Groups"]);
    public string StudyEvents => GetPath(_config[$"{DirectoryName}:StudyEvents"]);
    public string ExamEvents => GetPath(_config[$"{DirectoryName}:ExamEvents"]);
}