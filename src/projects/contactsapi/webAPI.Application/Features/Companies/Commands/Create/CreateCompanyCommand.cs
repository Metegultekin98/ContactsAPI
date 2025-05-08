using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Companies.Commands.Create;

public class CreateCompanyCommand : IRequest<CustomResponseDto<CreatedCompanyResponse>>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    
    public CreateCompanyCommand()
    {
        Name = string.Empty;
        Address = string.Empty;
        PhoneNumber = string.Empty;
    }
    
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CustomResponseDto<CreatedCompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CreatedCompanyResponse>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = _mapper.Map<Company>(request);
            
            Company createdCompany = await _companyRepository.AddAsync(company);

            CreatedCompanyResponse response = _mapper.Map<CreatedCompanyResponse>(createdCompany);
            return CustomResponseDto<CreatedCompanyResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}