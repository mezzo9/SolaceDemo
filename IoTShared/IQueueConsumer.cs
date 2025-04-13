using IoTShared.Devices;

namespace IoTShared;

public interface IQueueConsumer
{
    public string Queue { get; set; }
    void Consume(string content);
}
