using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QuickstartWeatherServer.Tools;
using System.Net.Http.Headers;
//based on samples from https://github.com/modelcontextprotocol/csharp-sdk
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<WeatherTools>();

builder.Services.AddSingleton(_ =>
{
    var client = new HttpClient() { BaseAddress = new Uri("https://api.weather.gov") };
    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("weather-tool", "1.0"));
    return client;
});

var app = builder.Build();
Console.WriteLine("Running");
app.MapMcp();
await app.RunAsync();