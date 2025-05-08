using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.ContactInfos.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.ContactInfos.Commands.Delete;

public class DeleteContactInfoCommand : IRequest<CustomResponseDto<DeletedContactInfoResponse>>
{
    public Guid Id { get; set; }

    public class DeleteContactInfoCommandHandler : IRequestHandler<DeleteContactInfoCommand, CustomResponseDto<DeletedContactInfoResponse>>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;
        private readonly ContactInfoBusinessRules _contactInfoBusinessRules;

        public DeleteContactInfoCommandHandler(IContactInfoRepository contactInfoRepository, IMapper mapper, ContactInfoBusinessRules contactInfoBusinessRules)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
            _contactInfoBusinessRules = contactInfoBusinessRules;
        }

        public async Task<CustomResponseDto<DeletedContactInfoResponse>> Handle(DeleteContactInfoCommand request, CancellationToken cancellationToken)
        {
            ContactInfo? contactInfo = await _contactInfoRepository.GetAsync(predicate: u => u.Id == request.Id, cancellationToken: cancellationToken);
            await _contactInfoBusinessRules.ContactInfoShouldBeExistsWhenSelected(contactInfo);

            await _contactInfoRepository.DeleteAsync(contactInfo!);

            DeletedContactInfoResponse response = _mapper.Map<DeletedContactInfoResponse>(contactInfo);
            return CustomResponseDto<DeletedContactInfoResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}