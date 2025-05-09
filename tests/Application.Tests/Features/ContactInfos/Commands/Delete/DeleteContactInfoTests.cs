using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.ContactInfos.Commands.Delete;
using Xunit;

namespace Application.Tests.Features.ContactInfos.Commands.Delete;

public class DeleteContactInfoTests : ContactInfoMockRepository
{
    private readonly DeleteContactInfoCommand.DeleteContactInfoCommandHandler _handler;
    private readonly DeleteContactInfoCommand _command;

    public DeleteContactInfoTests(IServiceProvider serviceProvider, ContactInfoFakeData fakeData, DeleteContactInfoCommand command)
        : base(serviceProvider, fakeData)
    {
        _command = command;
        _handler = new DeleteContactInfoCommand.DeleteContactInfoCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task DeleteShouldSuccessfully()
    {
        _command.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");
        CustomResponseDto<DeletedContactInfoResponse> result = await _handler.Handle(_command, CancellationToken.None);
        Assert.NotNull(result);
    }

}