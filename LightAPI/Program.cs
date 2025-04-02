using LightAPI;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddQuartz(config =>
{
    var jobKey = new JobKey(nameof(LightFlicker));
    config
        .AddJob<LightFlicker>(jobKey)
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
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Init();
app.Run();
