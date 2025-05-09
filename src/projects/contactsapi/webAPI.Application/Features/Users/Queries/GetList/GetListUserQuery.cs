using System.Net;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<CustomResponseDto<GetListResponse<GetListUserListItemDto>>>
{
    public PageRequest PageRequest { get; set; }

    public GetListUserQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListUserQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, CustomResponseDto<GetListResponse<GetListUserListItemDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<GetListResponse<GetListUserListItemDto>>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            IPaginate<User> users = await _userRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: x => x.Include(x => x.ContactInfos).Include(x => x.Company),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
            return CustomResponseDto<GetListResponse<GetListUserListItemDto>>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}