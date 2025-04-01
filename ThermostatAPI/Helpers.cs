namespace ThermostatAPI;

public static class Helpers
{
    public static IApplicationBuilder Init(this IApplicationBuilder app)
    {
        new Thermostats().Initialize();
        return app;
    }
}