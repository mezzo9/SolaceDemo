using System.ComponentModel;
using Solace.Shared;
namespace SmartPlugAPI;

public class AllDevices
{
    // public static readonly Location[] Hotels = [TestLocations.SFHilton, TestLocations.Marriott];
    public static readonly List<SmartPlug> SmartPlugs = new();
    
    private Location[] _locations = SetLocations();
    
    public void Initialize()
    {
        var rnd = new Random();
        // var room1 = Hotels[0].Buildings.First().Floors.First().Rooms.First();
        // room1.Devices.Add(GetSmartPlug(0.5, rnd));
        // room1.Devices.Add(GetSmartPlug(0.0, rnd));
        // room1.Devices.Add(GetSmartPlug(1, rnd));
        //
        // var room2 = Hotels[1].Buildings.ToArray()[1].Floors.First().Rooms.First();
        // room2.Devices.Add(GetSmartPlug(0.5, rnd));
        // room2.Devices.Add(GetSmartPlug(0.0, rnd));
        
        SmartPlugs.Add(GetSmartPlug(0.5, rnd));
        SmartPlugs.Add(GetSmartPlug(1, rnd));
        SmartPlugs.Add(GetSmartPlug(0.2, rnd));
        SmartPlugs.Add(GetSmartPlug(0.5, rnd));
        SmartPlugs.Add(GetSmartPlug(0.4, rnd));
        SmartPlugs.Add(GetSmartPlug(0.3, rnd));
    }

    private SmartPlug GetSmartPlug(double currentAmp, Random random)
    {
        var plug = new SmartPlug
        {
            Location = GetLocation(random.Next(0,2)),
            Metadata = new Metadata
            {
                Brand = Brand.Honeywell
            },
            IsActive = false,
            IsOnline = true,
            CurrentAmp = currentAmp,
            MaxAmp = 10,
        };
        plug.PropertyChanged += CurrentAmpChanged;
        return plug;
    }

    private Location GetLocation(int random)
    {
        return _locations[random];
    }

    private static Location[] SetLocations()
    {
        return
        [
            new Location
            {
                Address = "123 First Street, San Franciso, CA 95610",
                Name = "Hilton Hills",
                Buildings =
                [
                    new Building
                    {
                        Name = "Main",
                        Floors =
                        [
                            new Floor
                            {
                                Name = "First Floor",
                                FloorNumber = 1,
                                Rooms =
                                [
                                    new Room
                                    {
                                        Name = "101",
                                        NumberOfBeds = 1

                                    },
                                    new Room
                                    {
                                        Name = "102",
                                        NumberOfBeds = 2,
                                    },
                                    new Room
                                    {
                                        Name = "103",
                                        NumberOfBeds = 2,
                                    },
                                    new Room
                                    {
                                        Name = "104",
                                        NumberOfBeds = 1,
                                    }
                                ]
                            }
                        ]
                    }

                ]

            },
            new Location
            {
                Address = "4641 Lomas Blvd, Albuquerque, NM 87120",
                Name = "Marriott By Zia",
                Buildings =
                [
                    new Building
                    {
                        Name = "BuildingA",
                        Floors =
                        [
                            new Floor
                            {
                                Name = "First Floor",
                                FloorNumber = 1,
                                Rooms =
                                [
                                    new Room
                                    {
                                        Name = "Lobby",
                                        NumberOfBeds = 0,
                                        // Devices = new List<IDevice>()
                                    }
                                ]
                            }
                        ]
                    },
                    new Building
                    {
                        Name = "Building B",
                        Floors =
                        [
                            new Floor
                            {
                                Name = "Second Floor",
                                FloorNumber = 2,
                                Rooms =
                                [
                                    new Room
                                    {
                                        Name = "201",
                                        NumberOfBeds = 1,
                                        // Devices = new List<IDevice>()
                                    },
                                    new Room
                                    {
                                        Name = "202",
                                        NumberOfBeds = 2,
                                        // Devices = new List<IDevice>()
                                    },
                                    new Room
                                    {
                                        Name = "203",
                                        NumberOfBeds = 2,
                                        // Devices = new List<IDevice>()
                                    },
                                    new Room
                                    {
                                        Name = "204",
                                        NumberOfBeds = 1,
                                        // Devices = new List<IDevice>()
                                    }
                                ]
                            }
                        ]
                    }
                ]
            }
        ];
    }

    private void CurrentAmpChanged(object? sender, PropertyChangedEventArgs e)
    {
        Console.Out.WriteLine("AMP CHANGED!");
    }
}