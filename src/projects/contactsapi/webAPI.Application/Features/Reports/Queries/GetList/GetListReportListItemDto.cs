using Core.Application.Dtos;
using webAPI.Application.Features.Users.Queries.GetList;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.Reports.Queries.GetList;

public class GetListReportListItemDto : IDto
{
    public Guid Id { get; set; }
    public DateTime RequestedDate { get; set; }
    public string RequestedFor { get; set; }
    public ReportStatu ReportStatu { get; set; }
    public DateTime CreatedDate { get; set; }
    public IList<GetListUserListItemDto> Users { get; set; }
}