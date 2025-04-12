using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;

namespace NotificationAPI;

public class TooHotAlert: IConsumer
{
    public string Queue { get; set; } = Queues.Thermostats;

    public void Consume(string content)
    {
        var thermostat = JsonConvert.DeserializeObject<Thermostat>(content);
        if (thermostat == null) return;
        if(thermostat.CurrentTempreture > 90)
            new SendText().Send($"Thermostat in room: {thermostat.Room.Name} is showing current temperature of {thermostat.CurrentTempreture}");
    }
}