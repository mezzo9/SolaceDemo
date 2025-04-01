using Quartz;
using ThermostatAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();
app.UseHttpsRedirection();
app.Init();
app.Run();

