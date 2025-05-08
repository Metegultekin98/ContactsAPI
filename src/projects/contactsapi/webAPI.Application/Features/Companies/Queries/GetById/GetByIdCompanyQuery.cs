using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.Companies.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Companies.Queries.GetById;

public class GetByIdCompanyQuery : IRequest<CustomResponseDto<GetByIdCompanyResponse>>
{
    public Guid Id { get; set; }

    public class GetByIdCompanyQueryHandler : IRequestHandler<GetByIdCompanyQuery, CustomResponseDto<GetByIdCompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly CompanyBusinessRules _companyBusinessRules;

        public GetByIdCompanyQueryHandler(ICompanyRepository companyRepository, IMapper mapper, CompanyBusinessRules companyBusinessRules)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _companyBusinessRules = companyBusinessRules;
        }

        public async Task<CustomResponseDto<GetByIdCompanyResponse>> Handle(GetByIdCompanyQuery request, CancellationToken cancellationToken)
        {
            Company? company = await _companyRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _companyBusinessRules.CompanyShouldExistWhenSelected(company);

            GetByIdCompanyResponse response = _mapper.Map<GetByIdCompanyResponse>(company);
            return CustomResponseDto<GetByIdCompanyResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}