using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.Companies.Queries.GetList;
using Xunit;

namespace Application.Tests.Features.Companies.Queries.GetList;

public class GetListCompanyTests: CompanyMockRepository
{
    private readonly GetListCompanyQuery _query;
    private readonly GetListCompanyQuery.GetListCompanyQueryHandler _handler;

    public GetListCompanyTests(IServiceProvider serviceProvider, CompanyFakeData fakeData, GetListCompanyQuery query)
        : base(serviceProvider, fakeData)
    {
        _query = query;
        _handler = new GetListCompanyQuery.GetListCompanyQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetAllCompanysShouldSuccessfully()
    {
        _query.PageRequest = new PageRequest { PageIndex = 0, PageSize = 3 };

        CustomResponseDto<GetListResponse<GetListCompanyListItemDto>> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: 2, result.Data.Items.Count);
    }
}
