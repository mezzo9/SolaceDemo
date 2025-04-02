using IoTShared.Devices;

namespace IoTShared;

public interface IConsumer
{
    public string Queue { get; set; }
    void Consume(string content);
}
