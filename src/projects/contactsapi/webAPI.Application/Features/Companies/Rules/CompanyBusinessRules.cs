using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using webAPI.Application.Features.Companies.Constants;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Companies.Rules;

public class CompanyBusinessRules : BaseBusinessRules
{
    private readonly ICompanyRepository _companyRepository;
    public CompanyBusinessRules(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    public Task CompanyShouldExistWhenSelected(Company? company)
    {
        if (company == null)
            throw new BusinessException(CompaniesBusinessMessages.CompanyNotExists);
        return Task.CompletedTask;
    }
}