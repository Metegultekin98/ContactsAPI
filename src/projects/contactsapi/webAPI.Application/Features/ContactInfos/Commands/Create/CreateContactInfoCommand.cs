using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.ContactInfos.Commands.Create;

public class CreateContactInfoCommand : IRequest<CustomResponseDto<CreatedContactInfoResponse>>
{
    public string Type { get; set; }
    public string Value { get; set; }
    public Guid UserId { get; set; }

    public CreateContactInfoCommand()
    {
        Type = string.Empty;
        Value = string.Empty;
        UserId = Guid.Empty;
    }

    public class CreateContactInfoCommandHandler : IRequestHandler<CreateContactInfoCommand, CustomResponseDto<CreatedContactInfoResponse>>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;

        public CreateContactInfoCommandHandler(IContactInfoRepository contactInfoRepository, IMapper mapper)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CreatedContactInfoResponse>> Handle(CreateContactInfoCommand request, CancellationToken cancellationToken)
        {
            ContactInfo contactInfo = _mapper.Map<ContactInfo>(request);
            
            ContactInfo createdContactInfo = await _contactInfoRepository.AddAsync(contactInfo);

            CreatedContactInfoResponse response = _mapper.Map<CreatedContactInfoResponse>(createdContactInfo);
            return CustomResponseDto<CreatedContactInfoResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}