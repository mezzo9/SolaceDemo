using IoTShared.Devices;

namespace IoTShared;

public class SolaceEvent<T>
{
    public DateTime EventDateTime { get; set; }
    public required T Device { get; set; }
}