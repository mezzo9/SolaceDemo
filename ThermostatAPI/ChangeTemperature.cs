using Quartz;

namespace ThermostatAPI;
/// <summary>
/// This will simulate temperature readings between 50 to 80
/// </summary>
public class ChangeTemperature: IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var random = new Random();
        var newTemperature = random.Next(50, 80);
        
        // Randomly pick one of thermostats and changes its temperature to a random number   
        Thermostats.AllThermostats.ToArray()[random.Next(0, Thermostats.AllThermostats.Count)].CurrentTempreture = newTemperature;
            
        await Console.Out.WriteLineAsync($"Changed: {newTemperature}");
    }
    
}