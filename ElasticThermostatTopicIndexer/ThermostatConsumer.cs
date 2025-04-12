using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using IoTShared;
using IoTShared.Devices;
using Newtonsoft.Json;


namespace ElasticThermostatIndexer;

public class ThermostatConsumer : IConsumer
{
    private readonly ElasticsearchClient _client;
    public string Queue { get; set; } = Queues.Thermostats;

    public ThermostatConsumer()
    {
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .Authentication(new BasicAuthentication("elastic", "changeme"));
        _client = new ElasticsearchClient(settings);
    }
    
    public void Consume(string content)
    {
        var thermostat = JsonConvert.DeserializeObject<Thermostat>(content);
        _client.IndexAsync(thermostat, index: "iot").GetAwaiter().GetResult();
    }
    
}