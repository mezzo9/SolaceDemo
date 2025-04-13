using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;


namespace ElasticThermostatIndexer;

public class ThermostatConsumer : ITopicConsumer
{
    private readonly ElasticsearchClient _client;
    public string Topic { get; set; } = Topics.Thermostat;

    public ThermostatConsumer()
    {
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .Authentication(new BasicAuthentication("elastic", "changeme"));
        _client = new ElasticsearchClient(settings);
    }
    
    public void Consume(string content)
    {
        Console.Out.WriteLine($"Thermostat reading: {content}");
        var thermostat = JsonConvert.DeserializeObject<Thermostat>(content);
        _client.IndexAsync(thermostat, index: "iot").GetAwaiter().GetResult();
    }
    
}