using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;

namespace NotificationAPI;

/// <summary>
/// This consume events from Thermostats queue, and if temperature is above 90 will send a text message
/// </summary>
public class TooHotAlert: IQueueConsumer
{
    public string Queue { get; set; } = Queues.Alerts;

    public void Consume(string content)
    {
        var thermostat = JsonConvert.DeserializeObject<Thermostat>(content);
        if (thermostat == null) return;
        if(thermostat.CurrentTempreture > 90)
            new SendText().Send($"Thermostat in room: {thermostat.Room.Name} is showing current temperature of {thermostat.CurrentTempreture}");
    }
}