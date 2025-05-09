namespace Core.Utilities.MessageBrokers.RabbitMq;

public class MessageBrokerOptions
{
    public string HostName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public QueueOptions Queues { get; set; } = new QueueOptions();
}

public class QueueOptions
{
    public string NotificationQueue { get; set; } = string.Empty;
    public string EmailQueue { get; set; } = string.Empty;
    public string ReportQueue { get; set; } = string.Empty;
}