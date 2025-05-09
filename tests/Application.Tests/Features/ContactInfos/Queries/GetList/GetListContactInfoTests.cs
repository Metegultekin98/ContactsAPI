using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.ContactInfos.Queries.GetList;
using Xunit;

namespace Application.Tests.Features.ContactInfos.Queries.GetList;

public class GetListContactInfoTests : ContactInfoMockRepository
{
    private readonly GetListContactInfoQuery _query;
    private readonly GetListContactInfoQuery.GetListContactInfoQueryHandler _handler;

    public GetListContactInfoTests(IServiceProvider serviceProvider, ContactInfoFakeData fakeData, GetListContactInfoQuery query)
        : base(serviceProvider, fakeData)
    {
        _query = query;
        _handler = new GetListContactInfoQuery.GetListContactInfoQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetAllContactInfosShouldSuccessfully()
    {
        _query.PageRequest = new PageRequest { PageIndex = 0, PageSize = 3 };

        CustomResponseDto<GetListResponse<GetListContactInfoListItemDto>> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: 2, result.Data.Items.Count);
    }
}