using Quartz;

namespace LightAPI;
/// <summary>
/// This will simulate a light bulb turning on or off
/// </summary>
public class LightFlicker: IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        // picks a random light and flips the switch on that light
        var random = new Random();
        var lightNo = random.Next(0, Lights.AllBulbs.Count);
        var light = Lights.AllBulbs.ToArray()[lightNo];
        if (lightNo % 2 == 0)
        {
            light.IsOn = !light.IsOn;
            await Console.Out.WriteLineAsync($"light turned: {light.IsOn}");
        }
        else
        {
            light.IsOnline = !light.IsOnline;
            await Console.Out.WriteLineAsync($"light state is: {light.IsOnline}");
        }
    }

}