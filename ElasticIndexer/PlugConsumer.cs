using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;

namespace ElasticIndexer;

public class PlugConsumer : IQueueConsumer
{
    private readonly ElasticsearchClient _client;
    public string Queue { get; set; } = Queues.SmartPlugs;

    public PlugConsumer()
    {
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .Authentication(new BasicAuthentication("elastic", "changeme"));
        _client = new ElasticsearchClient(settings);
    }
    
    public void Consume(string content)
    {
        var plug = JsonConvert.DeserializeObject<SmartPlug>(content);
        _client.IndexAsync(plug, index: "iot").GetAwaiter().GetResult();
    }
}