using Quartz;
using SmartPlugAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Using Quartz to simulate readings from smart plugs Every 5 seconds 
builder.Services.AddQuartz(config =>
{
    var jobKey = new JobKey(nameof(ChangeLoad));

    config
        .AddJob<ChangeLoad>(jobKey)
        .AddTrigger(
            trigger => trigger.ForJob(jobKey)
                .StartNow()
                .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(5)
                        .RepeatForever()));

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

// Initializing all simulated Smart plugs 
app.Init();
app.Run();

