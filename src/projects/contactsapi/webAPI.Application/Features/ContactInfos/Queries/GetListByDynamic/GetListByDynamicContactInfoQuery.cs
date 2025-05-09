using System.Net;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using webAPI.Application.Features.ContactInfos.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.ContactInfos.Queries.GetListByDynamic;

public class GetListByDynamicContactInfoQuery : IRequest<CustomResponseDto<GetListResponse<GetListByDynamicContactInfoListItemDto>>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }
    public GetListByDynamicContactInfoQuery()
    {
        PageRequest = default!;
        DynamicQuery = default!;
    }

    public class GetListByDynamicContactInfoQueryHandler : IRequestHandler<GetListByDynamicContactInfoQuery,
        CustomResponseDto<GetListResponse<GetListByDynamicContactInfoListItemDto>>>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;
        private readonly ContactInfoBusinessRules _businessRules;

        public GetListByDynamicContactInfoQueryHandler(IContactInfoRepository contactInfoRepository, IMapper mapper, 
            ContactInfoBusinessRules businessRules)
        {
            _mapper = mapper;
            _contactInfoRepository = contactInfoRepository;
            _businessRules = businessRules;
        }

        public async Task<CustomResponseDto<GetListResponse<GetListByDynamicContactInfoListItemDto>>> Handle(
            GetListByDynamicContactInfoQuery request, CancellationToken cancellationToken)
        {
            var contactInfos = await _contactInfoRepository.GetListByDynamicAsync(
                request.DynamicQuery,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken);

            GetListResponse<GetListByDynamicContactInfoListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicContactInfoListItemDto>>(contactInfos);

            return CustomResponseDto<GetListResponse<GetListByDynamicContactInfoListItemDto>>.Success((int)HttpStatusCode.OK,
                response, true);
        }
    }
}
