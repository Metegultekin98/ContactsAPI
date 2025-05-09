namespace Core.Utilities.MessageBrokers.RabbitMq;

public interface IMessageBrokerHelper
{
    Task QueueMessageAsync(string queueName, object messageObject);
}