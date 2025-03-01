using Cache.Contracts.Services;
using StackExchange.Redis;

namespace Cache.Engine.Services;

public class CacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IConfiguration _configuration;

    public CacheService(IConfiguration configuration)
    {
        _redis = ConnectionMultiplexer.Connect(configuration["Cache:Redis:ConnectionString"]);
        _configuration = configuration;
    }

    public string GetBuildingById(long id)
    {
        throw new NotImplementedException();
    }
}