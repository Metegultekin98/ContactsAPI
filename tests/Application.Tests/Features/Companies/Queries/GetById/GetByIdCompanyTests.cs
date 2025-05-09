using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.Companies.Queries.GetById;
using Xunit;

namespace Application.Tests.Features.Companies.Queries.GetById;

public class GetByIdCompanyTests : CompanyMockRepository
{
    private readonly GetByIdCompanyQuery _query;
    private readonly GetByIdCompanyQuery.GetByIdCompanyQueryHandler _handler;

    public GetByIdCompanyTests(IServiceProvider serviceProvider, CompanyFakeData fakeData, GetByIdCompanyQuery query)
        : base(serviceProvider, fakeData)
    {
        _query = query;
        _handler = new GetByIdCompanyQuery.GetByIdCompanyQueryHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task GetByIdCompanyShouldSuccessfully()
    {
        _query.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");

        CustomResponseDto<GetByIdCompanyResponse> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: "Test Company1", result.Data.Name);
    }

}