using Quartz;

namespace ThermostatAPI;
/// <summary>
/// This will simulate temperature readings above 90. between 90 to 110
/// </summary>
public class TooHot: IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var random = new Random();
        var newTemperature = random.Next(90, 110);
        
        // Randomly pick one of thermostats and set its temperature   
        Thermostats.AllThermostats.ToArray()[random.Next(0, Thermostats.AllThermostats.Count)].CurrentTempreture = newTemperature;
            
        await Console.Out.WriteLineAsync($"Too Hot Temperature Changed: {newTemperature}");
    }
    
}