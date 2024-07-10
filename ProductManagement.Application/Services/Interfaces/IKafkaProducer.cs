namespace ProductManagement.Application.Services.Interfaces;

public interface IKafkaProducer
{
    Task ProduceAsync<T>(string topic, T message);
}