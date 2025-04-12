using Quartz;
using IoTShared;

namespace SmartPlugAPI;

/// <summary>
/// This will simulate load change on a plug, unit is AMP
/// </summary>
public class ChangeLoad : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var random = new Random();
        var newCurrent = Math.Round(random.NextDouble(),2);
        // Randomly pick one of smartplug and changes its current   
        SmartPlugs.AllSmartPlugs.ToArray()[random.Next(0, SmartPlugs.AllSmartPlugs.Count)].CurrentAmp = newCurrent;
            
        await Console.Out.WriteLineAsync($"Changed: {newCurrent}");
    }
}