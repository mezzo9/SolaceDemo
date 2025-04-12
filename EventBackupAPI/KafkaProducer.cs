namespace EventBackupAPI;
using Confluent.Kafka;

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
        var result = _producer.ProduceAsync(
            "device_archive", new Message<Null, string> { Value = content }).GetAwaiter().GetResult();
    }
}