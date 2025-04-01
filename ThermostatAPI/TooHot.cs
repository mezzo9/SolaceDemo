using Quartz;

namespace ThermostatAPI;

public class TooHot: IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var random = new Random();
        var newTemperature = random.Next(90, 110);
        Thermostats.AllThermostats.ToArray()[random.Next(0, Thermostats.AllThermostats.Count)].CurrentTempreture = newTemperature;
            
        await Console.Out.WriteLineAsync($"Too Hot Temperature Changed: {newTemperature}");
    }
    
}