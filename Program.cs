using Test;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<GuapApiService>();
builder.Services.AddScoped<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost:6379"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

// var guapApiService = app.Services.GetRequiredService<GuapApiService>();
// Console.WriteLine("Downloading version...");
// var test = await guapApiService.GetVersionAsync();
// Console.WriteLine($"Version downloaded successfully.");

// Console.WriteLine($"Downloading studies...");
// var test = await guapApiService.GetAllStudiesAsync();
// Console.WriteLine($"Studies downloaded successfully. {test.Count} studies.");

app.Run();