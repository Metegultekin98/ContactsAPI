using Application.Tests.Mocks.FakeData;
using Core.Domain.Entities;
using Core.Test.Application.Repositories;
using webAPI.Application.Features.Companies.Profiles;
using webAPI.Application.Features.Companies.Rules;
using webAPI.Application.Services.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class CompanyMockRepository : BaseMockRepository<ICompanyRepository, Company, Guid, MappingProfiles, CompanyBusinessRules, CompanyFakeData>
{
    public CompanyMockRepository(IServiceProvider serviceProvider, CompanyFakeData fakeData)
        : base(serviceProvider, fakeData) { }
}
