using Microsoft.AspNetCore.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SmartPlugAPI>("smartplugapi");

var thermostatapi = builder.AddProject<Projects.ThermostatAPI>("thermostatapi");

builder.AddProject<Projects.LightAPI>("lightapi");

builder.AddProject<Projects.ElasticIndexer>("elasticindexer");

builder.AddProject<Projects.EventBackupAPI>("backupapi");
builder.AddProject<Projects.ElasticThermostatTopicIndexer>("thermostatTopicIndexer");
builder.AddProject<Projects.NotificationAPI>("notificationapi").WaitFor(thermostatapi);

builder.Build().Run();