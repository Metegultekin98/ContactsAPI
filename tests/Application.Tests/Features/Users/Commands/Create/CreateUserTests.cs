using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using webAPI.Application.Features.Users.Commands.Create;
using static webAPI.Application.Features.Users.Commands.Create.CreateUserCommand;
using Xunit;


namespace Application.Tests.Features.Users.Commands.Create;

public class CreateUserTests : UserMockRepository
{
    private readonly CreateUserCommandValidator _validator;
    private readonly CreateUserCommand _command;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserTests(IServiceProvider serviceProvider,
        UserFakeData fakeData,
        CreateUserCommandValidator validator,
        CreateUserCommand command)
        : base(serviceProvider, fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new CreateUserCommandHandler(MockRepository.Object, Mapper, BusinessRules);
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
    public async Task CreateShouldSuccessfully()
    {
        _command.FirstName = "First";
        _command.LastName = "Last";
        _command.CompanyId = Guid.NewGuid();

        CustomResponseDto<CreatedUserResponse> result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "First", result.Data.FirstName);
    }
}
