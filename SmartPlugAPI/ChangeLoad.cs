using Quartz;
using Solace.Shared;

namespace SmartPlugAPI;

public class ChangeLoad : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        // var room1Plugs = AllHotels.Hotels[0].Buildings.First().Floors.First().Rooms.First().Devices
        //     .ToList().OfType<SmartPlug>().ToArray();
        var random = new Random();
        var newCurrent = Math.Round(random.NextDouble(),2);
        AllDevices.SmartPlugs.ToArray()[random.Next(0, AllDevices.SmartPlugs.Count)].CurrentAmp = newCurrent;
            
        await Console.Out.WriteLineAsync($"Changed: {newCurrent}");
    }
}