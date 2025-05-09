using Core.Application.Responses;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.Reports.Queries.GetById;

public class GetByIdReportResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime RequestedDate { get; set; }
    public string RequestedFor { get; set; }
    public ReportStatu ReportStatu { get; set; }
    public DateTime CreatedDate { get; set; }
}