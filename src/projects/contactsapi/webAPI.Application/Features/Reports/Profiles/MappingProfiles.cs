using AutoMapper;
using Core.Application.Responses;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using webAPI.Application.Features.Reports.Dtos.UserReport;
using webAPI.Application.Features.Reports.Queries.GetById;
using webAPI.Application.Features.Reports.Queries.GetList;

namespace webAPI.Application.Features.Reports.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Report, GetByIdReportResponse>().ReverseMap();
        CreateMap<Report, GetListReportListItemDto>().ReverseMap();
        CreateMap<IPaginate<Report>, GetListResponse<GetListReportListItemDto>>().ReverseMap();
    }
}