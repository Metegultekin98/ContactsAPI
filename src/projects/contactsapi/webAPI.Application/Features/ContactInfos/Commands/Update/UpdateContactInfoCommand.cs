using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.ContactInfos.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.ContactInfos.Commands.Update;

public class UpdateContactInfoCommand : IRequest<CustomResponseDto<UpdatedContactInfoResponse>>
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    public Guid UserId { get; set; }

    public UpdateContactInfoCommand()
    {
        Type = string.Empty;
        Value = string.Empty;
        UserId = Guid.Empty;
    }

    public class UpdateContactInfoCommandHandler : IRequestHandler<UpdateContactInfoCommand, CustomResponseDto<UpdatedContactInfoResponse>>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;
        private readonly ContactInfoBusinessRules _contactInfoBusinessRules;

        public UpdateContactInfoCommandHandler(IContactInfoRepository contactInfoRepository, IMapper mapper, ContactInfoBusinessRules contactInfoBusinessRules)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
            _contactInfoBusinessRules = contactInfoBusinessRules;
        }

        public async Task<CustomResponseDto<UpdatedContactInfoResponse>> Handle(UpdateContactInfoCommand request, CancellationToken cancellationToken)
        {
            ContactInfo? contactInfo = await _contactInfoRepository.GetAsync(predicate: u => u.Id == request.Id, cancellationToken: cancellationToken);
            await _contactInfoBusinessRules.ContactInfoShouldBeExistsWhenSelected(contactInfo);
            contactInfo = _mapper.Map(request, contactInfo);

            await _contactInfoRepository.UpdateAsync(contactInfo);

            UpdatedContactInfoResponse response = _mapper.Map<UpdatedContactInfoResponse>(contactInfo);
            return CustomResponseDto<UpdatedContactInfoResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}