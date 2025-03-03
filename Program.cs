using Application;
using Application.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<Client>();
builder.Services.AddScoped<Endpoints>();
builder.Services.AddScoped<IDatabase>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")).GetDatabase());
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();