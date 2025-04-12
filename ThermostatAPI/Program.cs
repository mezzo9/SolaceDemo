using Quartz;
using ThermostatAPI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

// Using Quartz to simulate Temperature readings and specific intervals
// Every 10 seconds a normal temperature reading will happen
// Every 30 seconds a random Thermostat will read a temperature between 90 to 110
builder.Services.AddQuartz(config =>
{
    var jobKey = new JobKey(nameof(ChangeTemperature));
    var hotJob = new JobKey(nameof(TooHot));
    config
        .AddJob<ChangeTemperature>(jobKey)
        .AddTrigger(
            trigger => trigger.ForJob(jobKey)
                .StartNow()
                .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10)
                    .RepeatForever()))
        .AddJob<TooHot>(hotJob)
        .AddTrigger( trigger => trigger.ForJob(hotJob)
            .WithSimpleSchedule( s=> s.WithIntervalInSeconds(30)
                .WithRepeatCount(6)));
});

builder.Services.AddQuartzHostedService(config =>
{
    config.WaitForJobsToComplete = true;
});

builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();
app.UseHttpsRedirection();

// Initializing all simulated Thermostats 
app.Init();
app.Run();

