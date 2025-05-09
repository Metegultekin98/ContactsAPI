using AutoMapper;
using Core.Application.Responses;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using webAPI.Application.Features.Reports.Dtos.UserReport;
using webAPI.Application.Features.Users.Commands.Create;
using webAPI.Application.Features.Users.Commands.Delete;
using webAPI.Application.Features.Users.Commands.Update;
using webAPI.Application.Features.Users.Queries.GetById;
using webAPI.Application.Features.Users.Queries.GetList;

namespace webAPI.Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, CreatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, DeletedUserResponse>().ReverseMap();
        CreateMap<User, GetByIdUserResponse>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<UserReportItemDto>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<User, UserReportItemDto>();
    }
}