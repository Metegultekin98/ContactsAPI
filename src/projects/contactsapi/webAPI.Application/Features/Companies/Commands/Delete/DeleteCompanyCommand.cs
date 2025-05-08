using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.Companies.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Companies.Commands.Delete;

public class DeleteCompanyCommand : IRequest<CustomResponseDto<DeletedCompanyResponse>>
{
    public Guid Id { get; set; }

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, CustomResponseDto<DeletedCompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly CompanyBusinessRules _companyBusinessRules;

        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper, CompanyBusinessRules companyBusinessRules)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _companyBusinessRules = companyBusinessRules;
        }

        public async Task<CustomResponseDto<DeletedCompanyResponse>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _companyRepository.GetAsync(predicate: u => u.Id == request.Id, cancellationToken: cancellationToken);
            await _companyBusinessRules.CompanyShouldExistWhenSelected(company);

            await _companyRepository.DeleteAsync(company!);

            DeletedCompanyResponse response = _mapper.Map<DeletedCompanyResponse>(company);
            return CustomResponseDto<DeletedCompanyResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}