namespace Solace.Shared;

public static class TestLocations
{
    public static Location SFHilton = new Location
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
                                NumberOfBeds = 1,
                                // // Devices = new List<IDevice>()
                            },
                            new Room
                            {
                                Name = "102",
                                NumberOfBeds = 2,
                                // Devices = new List<IDevice>()
                            },
                            new Room
                            {
                                Name = "103",
                                NumberOfBeds = 2,
                                // Devices = new List<IDevice>()
                            },
                            new Room
                            {
                                Name = "104",
                                NumberOfBeds = 1,
                                // Devices = new List<IDevice>()
                            }
                        ]
                    }
                ]
            }
        ]
    };

    public static Location Marriott = new Location
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
    };
}