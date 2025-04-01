var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SmartPlugAPI>("smartplugapi");

builder.AddProject<Projects.ThermostatAPI>("thermostatapi");

builder.Build().Run();