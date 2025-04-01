namespace IoTShared;

public class Floor
{
    public required int FloorId { get; set; }
    public required Building Building { get; set; }
    public required string Name { get; set; }
    public int FloorNumber { get; set; }
    
}