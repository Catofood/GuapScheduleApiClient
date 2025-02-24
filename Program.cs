using Test;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<GuapApiService>();
var app = builder.Build();

Console.WriteLine("Connecting to redis");
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
Console.WriteLine("Redis connected\n\n");

var guapApiService = app.Services.GetRequiredService<GuapApiService>();
// Console.WriteLine("Downloading version...");
// var test = await guapApiService.GetVersionAsync();
// Console.WriteLine($"Version downloaded successfully.");

Console.WriteLine($"Downloading studies...");
var test = await guapApiService.GetAllStudiesAsync();
Console.WriteLine($"Studies downloaded successfully. {test.Count} studies.");

app.Run();