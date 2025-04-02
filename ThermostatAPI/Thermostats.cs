using System.ComponentModel;
using System.Globalization;
using IoTShared;
using IoTShared.Devices;
using SolaceService;

namespace ThermostatAPI;

public class Thermostats
{
    // List of all the Thermostats
    public static readonly List<Thermostat> AllThermostats = new();
    private readonly Room[] _rooms = TestLocations.GetRooms();
    
    // This is a very bad idea, besides being bad design, it will cause memory growth
    // add it to DI as Singleton
    private readonly MessageService _messageService = new();

    public void Initialize()
    {
        var rnd = new Random();
        TestLocations.GetRooms();
        foreach (var n in Enumerable.Range(0,19))
            AllThermostats.Add(CreateThermostat(n, rnd));
    }

    private Thermostat CreateThermostat(int roomNo, Random random)
    {
        var thermostat = new Thermostat
        {
            DeviceId = roomNo,
            Room = _rooms[roomNo], 
            Metadata = new Metadata
            {
                Brand = Brand.Blink
            },
            IsOnline = true,
            CurrentTempreture = random.Next(50,80) 
        };
        thermostat.PropertyChanged += TemperatureChanged;
        return thermostat;
    }

    private void TemperatureChanged(object? sender, PropertyChangedEventArgs e)
    {
        // TODO: Fire Solace Event for changed thingy
        var thermostat = sender as Thermostat ?? CreateThermostat(0, new Random());
        var topic =
            $"device/thermostat/{thermostat.Room.Floor.Building.Location.Name}/{thermostat.Room.Floor.Building.Name}/{thermostat.Room.Floor.Name}/{thermostat.Room.Name}/{thermostat.Metadata.Brand}/temperature_changed";
        thermostat.ChangedAt = DateTime.UtcNow;
        _messageService.PublishMessage($"Thermostate temperature changed to: {thermostat.CurrentTempreture}",topic);
        Console.Out.WriteLine($"{topic}: {thermostat.CurrentTempreture}");
    }
}