using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.Reports.Queries.GetList;
using Xunit;

namespace Application.Tests.Features.Reports.Queries.GetList;

public class GetListReportTests : ReportMockRepository
{
    private readonly GetListReportQuery _query;
    private readonly GetListReportQuery.GetListReportQueryHandler _handler;

    public GetListReportTests(IServiceProvider serviceProvider, ReportFakeData fakeData, GetListReportQuery query)
        : base(serviceProvider, fakeData)
    {
        _query = query;
        _handler = new GetListReportQuery.GetListReportQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetAllReportsShouldSuccessfully()
    {
        _query.PageRequest = new PageRequest { PageIndex = 0, PageSize = 3 };

        CustomResponseDto<GetListResponse<GetListReportListItemDto>> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: 2, result.Data.Items.Count);
    }
}
