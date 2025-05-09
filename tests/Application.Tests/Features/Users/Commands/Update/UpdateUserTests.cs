using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using webAPI.Application.Features.Users.Commands.Update;
using Xunit;

namespace Application.Tests.Features.Users.Commands.Update;

public class UpdateUserTests : UserMockRepository
{
    private readonly UpdateUserCommandValidator _validator;
    private readonly UpdateUserCommand _command;
    private readonly UpdateUserCommand.UpdateUserCommandHandler _handler;

    public UpdateUserTests(IServiceProvider serviceProvider, UserFakeData fakeData, UpdateUserCommandValidator validator, UpdateUserCommand command)
        : base(serviceProvider, fakeData)
    {
        _validator = validator;
        _command = command;
        _handler = new UpdateUserCommand.UpdateUserCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public void UserCompanyEmptyShouldReturnError()
    {
        _command.CompanyId = Guid.Empty;

        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.FirstOrDefault(x => x.PropertyName == "CompanyId" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator);

        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact]
    public async Task UpdateShouldSuccessfully()
    {
        _command.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");
        _command.FirstName = "First";
        _command.LastName = "Last";
        _command.CompanyId = Guid.NewGuid();

        CustomResponseDto<UpdatedUserResponse> result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "First", result.Data.FirstName);
    }

}