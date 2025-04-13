namespace EventBackupAPI;
using Confluent.Kafka;

/// <summary>
/// Produces events for Kafka broker
/// basically reads the events from solace and republishes them to kafka
/// </summary>
public class KafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public void Archive(string content)
    {
        // Archives in kafka under topic: device_archive
        var result = _producer.ProduceAsync(
            "device_archive", new Message<Null, string> { Value = content }).GetAwaiter().GetResult();
    }
}