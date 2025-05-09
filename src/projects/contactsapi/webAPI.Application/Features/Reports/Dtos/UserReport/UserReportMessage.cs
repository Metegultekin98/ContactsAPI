using Core.Utilities.MessageBrokers.RabbitMq;

namespace webAPI.Application.Features.Reports.Dtos.UserReport;

public class UserReportMessage : IHasItems<UserReportItemDto>
{
    public IList<UserReportItemDto> Items { get; set; } = [];
    public Guid ReportId { get; set; }
}