namespace Campanhas.Service.Microservice.Domain.Interfaces;

public interface IRabbitMqPublisher
{
    Task PublishAsync<T>(T message);
}