using System.ComponentModel;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;
using SolaceService;

namespace LightAPI;

public class Lights
{
    // List of all the Lights in the system
    public static readonly List<Bulb> AllBulbs = new();
    private readonly Room[] _rooms = TestLocations.GetRooms();
    
    // This is a very bad idea, besides being bad design, it will cause memory growth
    // add it to DI as Singleton
    private readonly MessageService _messageService = new();
    
    public void Initialize()
    {
        var rnd = new Random();
        TestLocations.GetRooms();
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
                DeviceId = roomNo+i,
                Room = _rooms[roomNo],
                Metadata = new Metadata
                {
                    Brand = Brand.Philips
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
        // Fire Solace Event for changed thingy
        var bulb = sender as Bulb ?? ZeroLight;
        var topic =
            $"device/light/{bulb.Room.Floor.Building.Location.Name}/{bulb.Room.Floor.Building.Name}/{bulb.Room.Floor.Name}/{bulb.Room.Name}/{bulb.Metadata.Brand}/state_changed";
        //var sEvent = new SolaceEvent<Bulb> { EventDateTime = DateTime.Now.ToLocalTime(), Device = bulb};
        bulb.ChangedAt = DateTime.UtcNow;
        _messageService.PublishMessage(JsonConvert.SerializeObject(bulb),topic);
        Console.Out.WriteLine($"{topic}: {bulb.IsOn}");
    }

    private Bulb ZeroLight =>
        new Bulb
        {
            DeviceId = 0,
            Room = _rooms[0],
            Metadata = new Metadata
            {
                Brand = Brand.Philips
            },
            IsOnline = false,
            IsOn = false
        };
    
}