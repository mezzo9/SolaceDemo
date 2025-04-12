using System.ComponentModel;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;
using SolaceService;

namespace LightAPI;

public class Lights
{
    // List of all the Lights in the system
    public static readonly List<Bulb> AllBulbs = [];
    public readonly Room[] Rooms = TestLocations.GetRooms();
    private readonly MessageService _messageService = new();
    
    public void Initialize()
    {
        var rnd = new Random();
        foreach (var n in Enumerable.Range(0,19))
            AllBulbs.AddRange(CreateLight(n, rnd));
    }

    private List<Bulb> CreateLight(int roomNo, Random random)
    {
        var list = new List<Bulb>();
        foreach (var i in Enumerable.Range(0, random.Next(1, 6)))
        {
            var light = new Bulb
            {
                Domain = Domains.Lighting,
                DeviceId = roomNo+i,
                Room = Rooms[roomNo],
                Metadata = new Metadata
                {
                    Brand = Brand.Philips,
                    ModelNo = "PH-4251"
                },
                IsOnline = true,
                IsOn = roomNo % 2 == 0
            };
            light.PropertyChanged += LightChanges;
            list.Add(light);
        }

        return list;
    }

    private void LightChanges(object? sender, PropertyChangedEventArgs e)
    {
        var bulb = sender as Bulb ?? Darkness;
        var topic =
            $"cubanholding/uswest/{bulb.Room.Floor.Building.Location.Name}/{bulb.Room.Floor.Building.Location.Name}/" +
            $"{bulb.Room.Floor.Building.Name}/{bulb.Room.Floor.Name}/{bulb.Room.Name}" +
            $"/v1/iot/light/{bulb.Metadata.ModelNo}/temperature_changed";
        bulb.ChangedAt = DateTime.UtcNow;
        _messageService.PublishMessage(JsonConvert.SerializeObject(bulb),topic);
        Console.Out.WriteLine($"{topic}: {bulb.IsOn}");
    }

    private Bulb Darkness =>
        new()
        {
            DeviceId = 0,
            Room = Rooms[0],
            Metadata = new Metadata
            {
                Brand = Brand.Philips,
                ModelNo = "PH-0"
            },
            IsOnline = false,
            IsOn = false
        };
    
}