using Application.Models;

namespace Application;

public class RedisService
{
    private readonly IServiceScopeFactory _scopeFactory;
    RedisService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    // TODO: Перекинуть методы для работы с REDIS из GuapApiService сюда
}