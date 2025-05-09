using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.ContactInfos.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.ContactInfos.Queries.GetByUserId;

public class GetByUserIdContactInfoQuery : IRequest<CustomResponseDto<GetByUserIdContactInfoResponse>>
{
    public Guid UserId { get; set; }

    public class GetByUserIdContactInfoQueryHandler : IRequestHandler<GetByUserIdContactInfoQuery, CustomResponseDto<GetByUserIdContactInfoResponse>>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;
        private readonly ContactInfoBusinessRules _contactInfoBusinessRules;

        public GetByUserIdContactInfoQueryHandler(IContactInfoRepository contactInfoRepository, IMapper mapper, ContactInfoBusinessRules contactInfoBusinessRules)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
            _contactInfoBusinessRules = contactInfoBusinessRules;
        }

        public async Task<CustomResponseDto<GetByUserIdContactInfoResponse>> Handle(GetByUserIdContactInfoQuery request, CancellationToken cancellationToken)
        {
            ContactInfo? contactInfo = await _contactInfoRepository.GetAsync(predicate: b => b.UserId == request.UserId, cancellationToken: cancellationToken);
            await _contactInfoBusinessRules.ContactInfoShouldBeExistsWhenSelected(contactInfo);

            GetByUserIdContactInfoResponse response = _mapper.Map<GetByUserIdContactInfoResponse>(contactInfo);
            return CustomResponseDto<GetByUserIdContactInfoResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}