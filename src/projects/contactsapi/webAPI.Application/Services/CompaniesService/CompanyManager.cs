using System.Linq.Expressions;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Services.CompaniesService;

public class CompanyManager : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyManager(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Company?> GetAsync(
        Expression<Func<Company, bool>> predicate,
        Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Company? company = await _companyRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return company;
    }

    public async Task<IPaginate<Company>?> GetListAsync(
        Expression<Func<Company, bool>>? predicate = null,
        Func<IQueryable<Company>, IOrderedQueryable<Company>>? orderBy = null,
        Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Company> companyList = await _companyRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return companyList;
    }

    public async Task<Company> AddAsync(Company company)
    {
        Company addedCompany = await _companyRepository.AddAsync(company);

        return addedCompany;
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        Company updatedCompany = await _companyRepository.UpdateAsync(company);

        return updatedCompany;
    }

    public async Task<Company> DeleteAsync(Company company, bool permanent = false)
    {
        Company deletedCompany = await _companyRepository.DeleteAsync(company);

        return deletedCompany;
    }
}