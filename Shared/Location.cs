namespace Solace.Shared;

public class Location
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required IEnumerable<Building> Buildings { get; set; }
}