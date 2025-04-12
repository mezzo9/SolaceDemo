using IoTShared;

namespace EventBackupAPI;

public class BakcupDeviceEvents : IConsumer
{
    private readonly KafkaProducer _archiver = new();

    public string Queue { get; set; } = Queues.DeviceBackup;
    public void Consume(string content)
    {
        _archiver.Archive(content);
    }
}