using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.ContactInfos.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.ContactInfos.Queries.GetById;

public class GetByIdContactInfoQuery : IRequest<CustomResponseDto<GetByIdContactInfoResponse>>
{
    public Guid Id { get; set; }

    public class GetByIdContactInfoQueryHandler : IRequestHandler<GetByIdContactInfoQuery, CustomResponseDto<GetByIdContactInfoResponse>>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;
        private readonly ContactInfoBusinessRules _contactInfoBusinessRules;

        public GetByIdContactInfoQueryHandler(IContactInfoRepository contactInfoRepository, IMapper mapper, ContactInfoBusinessRules contactInfoBusinessRules)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
            _contactInfoBusinessRules = contactInfoBusinessRules;
        }

        public async Task<CustomResponseDto<GetByIdContactInfoResponse>> Handle(GetByIdContactInfoQuery request, CancellationToken cancellationToken)
        {
            ContactInfo? contactInfo = await _contactInfoRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _contactInfoBusinessRules.ContactInfoShouldBeExistsWhenSelected(contactInfo);

            GetByIdContactInfoResponse response = _mapper.Map<GetByIdContactInfoResponse>(contactInfo);
            return CustomResponseDto<GetByIdContactInfoResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}