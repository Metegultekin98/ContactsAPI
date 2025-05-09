using Core.Application.Dtos;

namespace webAPI.Application.Features.Reports.Dtos;

public class CreateReportDto : IDto
{
    public string RequestedFor { get; set; }
}