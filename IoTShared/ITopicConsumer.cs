namespace IoTShared;

public interface ITopicConsumer
{
    public string Topic { get; set; }
    void Consume(string content);
}