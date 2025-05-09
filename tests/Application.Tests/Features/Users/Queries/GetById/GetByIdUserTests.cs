using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.Users.Queries.GetById;
using Xunit;

namespace Application.Tests.Features.Users.Queries.GetById;

public class GetByIdUserTests : UserMockRepository
{
    private readonly GetByIdUserQuery _query;
    private readonly GetByIdUserQuery.GetByIdUserQueryHandler _handler;

    public GetByIdUserTests(IServiceProvider serviceProvider, UserFakeData fakeData, GetByIdUserQuery query)
        : base(serviceProvider, fakeData)
    {
        _query = query;
        _handler = new GetByIdUserQuery.GetByIdUserQueryHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task GetByIdUserShouldSuccessfully()
    {
        _query.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");

        CustomResponseDto<GetByIdUserResponse> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: "User1", result.Data.LastName);
    }

}