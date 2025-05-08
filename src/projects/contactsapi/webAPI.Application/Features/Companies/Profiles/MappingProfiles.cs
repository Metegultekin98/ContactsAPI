using AutoMapper;
using Core.Application.Responses;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using webAPI.Application.Features.Companies.Commands.Create;
using webAPI.Application.Features.Companies.Commands.Delete;
using webAPI.Application.Features.Companies.Commands.Update;
using webAPI.Application.Features.Companies.Queries.GetById;
using webAPI.Application.Features.Companies.Queries.GetList;

namespace webAPI.Application.Features.Companies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Company, CreateCompanyCommand>().ReverseMap();
        CreateMap<Company, CreatedCompanyResponse>().ReverseMap();
        CreateMap<Company, UpdateCompanyCommand>().ReverseMap();
        CreateMap<Company, UpdatedCompanyResponse>().ReverseMap();
        CreateMap<Company, DeleteCompanyCommand>().ReverseMap();
        CreateMap<Company, DeletedCompanyResponse>().ReverseMap();
        CreateMap<Company, GetByIdCompanyResponse>().ReverseMap();
        CreateMap<Company, GetListCompanyListItemDto>().ReverseMap();
        CreateMap<IPaginate<Company>, GetListResponse<GetListCompanyListItemDto>>().ReverseMap();
    }
}