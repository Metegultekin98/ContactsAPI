using System.Net;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using MediatR;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.ContactInfos.Queries.GetList;

public class GetListContactInfoQuery : IRequest<CustomResponseDto<GetListResponse<GetListContactInfoListItemDto>>>
{
    public PageRequest PageRequest { get; set; }

    public GetListContactInfoQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListContactInfoQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetListContactInfoQueryHandler : IRequestHandler<GetListContactInfoQuery, CustomResponseDto<GetListResponse<GetListContactInfoListItemDto>>>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;

        public GetListContactInfoQueryHandler(IContactInfoRepository contactInfoRepository, IMapper mapper)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<GetListResponse<GetListContactInfoListItemDto>>> Handle(GetListContactInfoQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContactInfo> contactInfos = await _contactInfoRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContactInfoListItemDto> response = _mapper.Map<GetListResponse<GetListContactInfoListItemDto>>(contactInfos);
            return CustomResponseDto<GetListResponse<GetListContactInfoListItemDto>>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}