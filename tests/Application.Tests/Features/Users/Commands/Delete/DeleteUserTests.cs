using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.Users.Commands.Delete;
using Xunit;

namespace Application.Tests.Features.Users.Commands.Delete;

public class DeleteUserTests : UserMockRepository
{
    private readonly DeleteUserCommand.DeleteUserCommandHandler _handler;
    private readonly DeleteUserCommand _command;

    public DeleteUserTests(IServiceProvider serviceProvider, UserFakeData fakeData, DeleteUserCommand command)
        : base(serviceProvider, fakeData)
    {
        _command = command;
        _handler = new DeleteUserCommand.DeleteUserCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task DeleteShouldSuccessfully()
    {
        _command.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");
        CustomResponseDto<DeletedUserResponse> result = await _handler.Handle(_command, CancellationToken.None);
        Assert.NotNull(result);
    }

}