using System.ComponentModel;
using System.Globalization;
using IoTShared;
using IoTShared.Devices;

namespace SmartPlugAPI;

public class AllDevices
{
    // List of all the SmartPlugs
    public static readonly List<SmartPlug> SmartPlugs = new();
    private readonly Room[] _rooms = TestLocations.GetRooms();
    public void Initialize()
    {
        var rnd = new Random();
        TestLocations.GetRooms();
        foreach (var n in Enumerable.Range(1,20))
            SmartPlugs.Add(CreateSmartPlug(n+5, rnd));
    }

    private SmartPlug CreateSmartPlug(double currentAmp, Random random)
    {
        // We have total of 20 rooms, it is hard coded in the TestLocations
        var roomNo = random.Next(0, 20);
        var plug = new SmartPlug
        {
            DeviceId = int.Parse(currentAmp.ToString(CultureInfo.InvariantCulture)),
            Room = _rooms[roomNo], 
            Metadata = new Metadata
            {
                Brand = Brand.Honeywell
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
        // TODO: Fire Solace Event for changed thingy
        // hub/client/location/room/component/status
        // Location/Building/Floor/Room/Sensor/State
        var plug = sender as SmartPlug ?? CreateSmartPlug(0, new Random());
        var topic =
            $"{plug.Room.Floor.Building.Location.Name}/{plug.Room.Floor.Building.Name}/{plug.Room.Floor.Name}/{plug.Room.Name}/smartplug/{plug.Metadata.Brand}/current_changed";
        Console.Out.WriteLine($"{topic}: {plug.CurrentAmp}");
    }
}