using Confluent.Kafka;
using Newtonsoft.Json;
using ProductManagement.Application.Services.Interfaces;

namespace ProductManagement.Infrastructure.Kafka;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(string bootstrapServers)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync<T>(string topic, T message)
    {
        var serializedMessage = JsonConvert.SerializeObject(message);
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedMessage });
    }
}