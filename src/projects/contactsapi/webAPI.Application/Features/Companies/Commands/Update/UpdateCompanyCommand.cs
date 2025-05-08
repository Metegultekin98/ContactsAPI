using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.Companies.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Companies.Commands.Update;

public class UpdateCompanyCommand : IRequest<CustomResponseDto<UpdatedCompanyResponse>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }

    public UpdateCompanyCommand()
    {
        Name = string.Empty;
        Address = string.Empty;
        PhoneNumber = string.Empty;
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CustomResponseDto<UpdatedCompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly CompanyBusinessRules _companyBusinessRules;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper, CompanyBusinessRules companyBusinessRules)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _companyBusinessRules = companyBusinessRules;
        }

        public async Task<CustomResponseDto<UpdatedCompanyResponse>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _companyRepository.GetAsync(predicate: u => u.Id == request.Id, cancellationToken: cancellationToken);
            await _companyBusinessRules.CompanyShouldExistWhenSelected(company);
            company = _mapper.Map(request, company);

            await _companyRepository.UpdateAsync(company);

            UpdatedCompanyResponse response = _mapper.Map<UpdatedCompanyResponse>(company);
            return CustomResponseDto<UpdatedCompanyResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}