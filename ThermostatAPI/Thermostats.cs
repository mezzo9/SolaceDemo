using System.ComponentModel;
using System.Globalization;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;
using SolaceService;

namespace ThermostatAPI;

public class Thermostats
{
    // List of all the Thermostats
    public static readonly List<Thermostat> AllThermostats = [];
    private readonly Room[] _rooms = TestLocations.GetRooms();
    
    private readonly MessageService _messageService = new();

    /// <summary>
    /// Adds one thermostat to every room
    /// </summary>
    public void Initialize()
    {
        var rnd = new Random();
        foreach (var n in Enumerable.Range(0,19))
            AllThermostats.Add(CreateThermostat(n, rnd));
    }

    private Thermostat CreateThermostat(int roomNo, Random random)
    {
        var thermostat = new Thermostat
        {
            DeviceId = roomNo,
            Room = _rooms[roomNo], 
            Domain = Domains.Thermostat,
            Metadata = new Metadata
            {
                Brand = Brand.Blink,
                ModelNo = "BL-90a534"
            },
            IsOnline = true,
            CurrentTempreture = random.Next(50,80) 
        };
        thermostat.PropertyChanged += TemperatureChanged;
        return thermostat;
    }

    private void TemperatureChanged(object? sender, PropertyChangedEventArgs e)
    {
// /client/region/hotel /location/building/floor/room/version/event_type/domain/modelno/[State, changed]
        var thermostat = sender as Thermostat ?? CreateThermostat(0, new Random());
        var topic =
            $"cubanholding/uswest/{thermostat.Room.Floor.Building.Location.Name}/{thermostat.Room.Floor.Building.Location.Name}/" +
            $"{thermostat.Room.Floor.Building.Name}/{thermostat.Room.Floor.Name}/{thermostat.Room.Name}" +
            $"/v1/iot/thermostat/{thermostat.Metadata.ModelNo}/temperature_changed";
        thermostat.ChangedAt = DateTime.UtcNow;
        _messageService.PublishMessage(JsonConvert.SerializeObject(thermostat),topic);
        Console.Out.WriteLine($"{topic}: {thermostat.CurrentTempreture}");
    }
}