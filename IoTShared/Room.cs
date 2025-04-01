namespace IoTShared;

public class Room
{
    public required int RoomId { get; set; } 
    public required Floor Floor { get; set; }
    public required string Name { get; set; }
    public int NumberOfBeds { get; set; }

}