namespace Solace.Shared;

public class Building
{
    public required string Name { get; set; }
    public required IEnumerable<Floor> Floors { get; set; }
}