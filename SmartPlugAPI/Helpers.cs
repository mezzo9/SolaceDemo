namespace SmartPlugAPI;

public static class Helpers
{
    public static IApplicationBuilder Init(this IApplicationBuilder app)
    {
        var hotels = new AllDevices();
        hotels.Initialize();
        return app;
    }
}