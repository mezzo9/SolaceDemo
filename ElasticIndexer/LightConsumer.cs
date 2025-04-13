using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;

namespace ElasticIndexer;

public class LighConsumer : IQueueConsumer
{
    private readonly ElasticsearchClient _client;
    public string Queue { get; set; } = Queues.Lights;

    public LighConsumer()
    {
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .Authentication(new BasicAuthentication("elastic", "changeme"));
         _client = new ElasticsearchClient(settings);
    }
    
    public void Consume(string content)
    {
        var bulb = JsonConvert.DeserializeObject<Bulb>(content);
        _client.IndexAsync(bulb, index: "iot").GetAwaiter().GetResult();
    }
}