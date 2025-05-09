using AutoMapper;
using Core.Application.Responses;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using webAPI.Application.Features.ContactInfos.Commands.Create;
using webAPI.Application.Features.ContactInfos.Commands.Delete;
using webAPI.Application.Features.ContactInfos.Commands.Update;
using webAPI.Application.Features.ContactInfos.Queries.GetById;
using webAPI.Application.Features.ContactInfos.Queries.GetByUserId;
using webAPI.Application.Features.ContactInfos.Queries.GetList;
using webAPI.Application.Features.ContactInfos.Queries.GetListByDynamic;

namespace webAPI.Application.Features.ContactInfos.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContactInfo, CreateContactInfoCommand>().ReverseMap();
        CreateMap<ContactInfo, CreatedContactInfoResponse>().ReverseMap();
        CreateMap<ContactInfo, UpdateContactInfoCommand>().ReverseMap();
        CreateMap<ContactInfo, UpdatedContactInfoResponse>().ReverseMap();
        CreateMap<ContactInfo, DeleteContactInfoCommand>().ReverseMap();
        CreateMap<ContactInfo, DeletedContactInfoResponse>().ReverseMap();
        CreateMap<ContactInfo, GetByIdContactInfoResponse>().ReverseMap();
        CreateMap<ContactInfo, GetByUserIdContactInfoResponse>().ReverseMap();
        CreateMap<ContactInfo, GetListContactInfoListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContactInfo>, GetListResponse<GetListContactInfoListItemDto>>().ReverseMap();
        CreateMap<ContactInfo, GetListByDynamicContactInfoListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContactInfo>, GetListResponse<GetListByDynamicContactInfoListItemDto>>().ReverseMap();
    }
}