using System.ComponentModel;
using System.Globalization;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;
using SolaceService;

namespace SmartPlugAPI;

public class SmartPlugs
{
    // List of all the SmartPlugs
    public static readonly List<SmartPlug> AllSmartPlugs = [];
    public readonly Room[] Rooms = TestLocations.GetRooms();
    private readonly MessageService _messageService = new();

    public void Initialize()
    {
        var rnd = new Random();
        foreach (var n in Enumerable.Range(1,20))
            AllSmartPlugs.Add(CreateSmartPlug(n+5, rnd));
    }

    private SmartPlug CreateSmartPlug(double currentAmp, Random random)
    {
        // We have total of 20 rooms, it is hard coded in the TestLocations
        var roomNo = random.Next(0, 20);
        var plug = new SmartPlug
        {
            DeviceId = int.Parse(currentAmp.ToString(CultureInfo.InvariantCulture)),
            Room = Rooms[roomNo],
            Domain = Domains.Plug,
            Metadata = new Metadata
            {
                Brand = Brand.Honeywell,
                ModelNo = "HW-213"
            },
            IsActive = roomNo % 2 == 0, // to have a sample of both active and non-active ones
            IsOnline = true,
            CurrentAmp = currentAmp,
            MaxAmp = 10,
        };
        plug.PropertyChanged += CurrentAmpChanged;
        return plug;
    }

    private void CurrentAmpChanged(object? sender, PropertyChangedEventArgs e)
    {

        // /client/region/hotel/location/building/floor/room/version/event_type/domain/modelno/[State, changed]

        var plug = sender as SmartPlug ?? CreateSmartPlug(0, new Random());
        var topic =
            $"cubanholding/uswest/{plug.Room.Floor.Building.Location.Name}/{plug.Room.Floor.Building.Location.Name}/" +
            $"{plug.Room.Floor.Building.Name}/{plug.Room.Floor.Name}/{plug.Room.Name}" +
            $"/v1/iot/smartplug/{plug.Metadata.ModelNo}/current_changed";
        plug.ChangedAt = DateTime.UtcNow;
        _messageService.PublishMessage(JsonConvert.SerializeObject(plug),topic);
        Console.Out.WriteLine($"{topic}: {plug.CurrentAmp}");
    }
}
