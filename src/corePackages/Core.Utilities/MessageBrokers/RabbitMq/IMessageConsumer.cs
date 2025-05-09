namespace Core.Utilities.MessageBrokers.RabbitMq;

public interface IMessageConsumer
{
    Task<string> GetQueueMessageAsync(string queueName);
    void AcknowledgeMessage(ulong deliveryTag);
    void RejectMessage(ulong deliveryTag);

}