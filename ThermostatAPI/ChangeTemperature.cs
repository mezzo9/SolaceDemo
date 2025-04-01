using Quartz;

namespace ThermostatAPI;

public class ChangeTemperature: IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var random = new Random();
        var newTemperature = random.Next(50, 80);
        Thermostats.AllThermostats.ToArray()[random.Next(0, Thermostats.AllThermostats.Count)].CurrentTempreture = newTemperature;
            
        await Console.Out.WriteLineAsync($"Changed: {newTemperature}");
    }
    
}