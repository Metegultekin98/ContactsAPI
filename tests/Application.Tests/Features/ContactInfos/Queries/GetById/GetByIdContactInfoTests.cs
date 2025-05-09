using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.ContactInfos.Queries.GetById;
using Xunit;

namespace Application.Tests.Features.ContactInfos.Queries.GetById;

public class GetByIdContactInfoTests : ContactInfoMockRepository
{
    private readonly GetByIdContactInfoQuery _query;
    private readonly GetByIdContactInfoQuery.GetByIdContactInfoQueryHandler _handler;

    public GetByIdContactInfoTests(IServiceProvider serviceProvider, ContactInfoFakeData fakeData, GetByIdContactInfoQuery query)
        : base(serviceProvider, fakeData)
    {
        _query = query;
        _handler = new GetByIdContactInfoQuery.GetByIdContactInfoQueryHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task GetByIdContactInfoShouldSuccessfully()
    {
        _query.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");

        CustomResponseDto<GetByIdContactInfoResponse> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: "test@test.com", result.Data.Value);
    }

}