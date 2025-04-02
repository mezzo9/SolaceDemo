using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;
using SolaceService;using SolaceSystems.Solclient.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");
new QueueConsumer().Consume(new LighConsumer());
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class LighConsumer : IConsumer
{
    public string Queue { get; set; } = "Q/lights";
    public void Consume(string content)
    {
        var bulb = JsonConvert.DeserializeObject<Bulb>(content);
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .Authentication(new BasicAuthentication("elastic", "changeme"));
        var client = new ElasticsearchClient(settings); // new ApiKey("PF9dZC_FRzGjg-MsGs4TrQ"));
        // var response = client.Indices.CreateAsync("iot").GetAwaiter().GetResult();

        var iresponse = client.IndexAsync(bulb, index: "iot").GetAwaiter().GetResult();
    }
}