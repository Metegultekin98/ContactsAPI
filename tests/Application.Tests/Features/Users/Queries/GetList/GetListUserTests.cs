using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.Users.Queries.GetList;
using Xunit;

namespace Application.Tests.Features.Users.Queries.GetList;

public class GetListUserTests : UserMockRepository
{
    private readonly GetListUserQuery _query;
    private readonly GetListUserQuery.GetListUserQueryHandler _handler;

    public GetListUserTests(IServiceProvider serviceProvider, UserFakeData fakeData, GetListUserQuery query)
        : base(serviceProvider, fakeData)
    {
        _query = query;
        _handler = new GetListUserQuery.GetListUserQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetAllUsersShouldSuccessfully()
    {
        _query.PageRequest = new PageRequest { PageIndex = 0, PageSize = 3 };

        CustomResponseDto<GetListResponse<GetListUserListItemDto>> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: 2, result.Data.Items.Count);
    }
}
