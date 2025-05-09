using Core.Application.Dtos;
using static Core.Domain.ComplexTypes.Enums;
using webAPI.Application.Features.Companies.Queries.GetList;
using webAPI.Application.Features.ContactInfos.Queries.GetList;

namespace webAPI.Application.Features.Reports.Dtos.UserReport;

public class UserReportItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public RecordStatu Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public GetListCompanyListItemDto Company { get; set; } = new();
    public IList<GetListContactInfoListItemDto> ContactInfos { get; set; }
}