namespace IoTShared;

public class Location
{
    public int LocationId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    // public required IEnumerable<Building> Buildings { get; set; }
}