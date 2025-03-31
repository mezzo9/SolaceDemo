namespace Solace.Shared;

public class Floor
{
    public required string Name { get; set; }
    public int FloorNumber { get; set; }
    public required IEnumerable<Room> Rooms { get; set; }
}