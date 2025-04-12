using NotificationAPI;
using SolaceService;

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

// Instantiating a new Thermostat consumer.
// If Temperature is higher than 90 it will send us an Alert
new QueueConsumer().Consume(new TooHotAlert());


/*
 * Other types of notifications can be configured here, for different devices or even different types of events.
 * Like guest check-ins or payments received.
 * Or if each of these have complex logic and customizations, can be separated into their own projects. 
 */


app.Run();

