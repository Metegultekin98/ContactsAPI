using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.Reports.Queries.GetById;
using Xunit;

namespace Application.Tests.Features.Reports.Queries.GetById;

public class GetByIdReportTests : ReportMockRepository
{
    private readonly GetByIdReportQuery _query;
    private readonly GetByIdReportQuery.GetByIdReportQueryHandler _handler;

    public GetByIdReportTests(IServiceProvider serviceProvider, ReportFakeData fakeData, GetByIdReportQuery query)
        : base(serviceProvider, fakeData)
    {
        _query = query;
        _handler = new GetByIdReportQuery.GetByIdReportQueryHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task GetByIdReportShouldSuccessfully()
    {
        _query.Id = Guid.Parse("00fba259-ad65-49ba-bb86-edc4bf6f27b6");

        CustomResponseDto<GetByIdReportResponse> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: "Test2", result.Data.RequestedFor);
    }

}