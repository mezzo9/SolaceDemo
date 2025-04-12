namespace SmartPlugAPI;

public static class Helpers
{
    public static IApplicationBuilder Init(this IApplicationBuilder app)
    {
        new SmartPlugs().Initialize();
        return app;
    }
}