namespace LightAPI;

public static class Helpers
{
    public static IApplicationBuilder Init(this IApplicationBuilder app)
    {
        new Lights().Initialize();
        return app;
    }
}