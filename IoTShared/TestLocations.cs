namespace IoTShared;

public static class TestLocations
{
    private static Room[]? _rooms;
    public static readonly Location SFHilton = new Location
    {
        LocationId = 1,
        Address = "123 First Street, San Francisco, CA 95610",
        Name = "Hilton Hills"
    };

    public static readonly Location Marriott = new Location
    {
        LocationId = 2,
        Address = "4641 Lomas Blvd, Albuquerque, NM 87120",
        Name = "Marriott By Zia"
    };

    public static readonly Location WestPoint = new Location
    {
        LocationId = 3,
        Address = "1829 Sacramento St, New York, NY 54123",
        Name = "West Sea Point"
    };
    
    public static readonly List<Floor> Floors =
    [
        new Floor
        {
            Building = new Building
            {
                Location = SFHilton,
                Name = "Main",
                BuildingId = 1
            },
            Name = "First Floor",
            FloorId = 1,
            FloorNumber = 1
        },

        new Floor
        {
            Building = new Building
            {
                Location = Marriott,
                Name = "Aztec",
                BuildingId = 2
            },
            Name = "Second Floor",
            FloorId = 2,
            FloorNumber = 2
        },

        new Floor
        {
            Building = new Building
            {
                Location = WestPoint,
                Name = "Convention Center",
                BuildingId = 3
            },
            Name = "Third Floor",
            FloorId = 3,
            FloorNumber = 3
        }
    ];
    

    
    public static Room[] GetRooms()
    {
        int howMany = 20;
        if (_rooms == null)
            _rooms = Enumerable.Range(start:0, count:howMany).Select(_ => GenerateRoom(new Random())).ToArray();
        return _rooms;
    }

    private static Room GenerateRoom(Random random)
    {
        var roomNumber = random.Next(1, 10);
        return new Room
        {
            Floor = Floors.FirstOrDefault(f => f.FloorNumber == Math.Abs(roomNumber / 3)) ?? Lobby(),
            Name = $"{roomNumber}",
            RoomId = int.Parse($"{Math.Abs(roomNumber / 3)}{roomNumber}"),
            NumberOfBeds = random.Next(1, 3)
        };
    }

    private static Floor Lobby()
    {
        return new Floor
        {
            Building = new Building
            {
                Location = WestPoint,
                Name = "Convention Center",
                BuildingId = 3
            },
            Name = "Lobby",
            FloorId = 4,
            FloorNumber = 1
        };
    }
}