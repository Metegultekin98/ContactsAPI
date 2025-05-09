namespace Core.Utilities.MessageBrokers;

public class QueueMessageWithTag
{
    public string Message { get; set; } = string.Empty;
    public ulong DeliveryTag { get; set; }
}