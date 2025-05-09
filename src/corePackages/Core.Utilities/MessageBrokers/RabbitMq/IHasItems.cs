namespace Core.Utilities.MessageBrokers.RabbitMq;

public interface IHasItems<TItem>
{
    IList<TItem> Items { get; set; }
    public Guid ReportId { get; set; }
}