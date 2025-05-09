using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using webAPI.Application.Features.ContactInfos.Commands.Create;
using Xunit;

namespace Application.Tests.Features.ContactInfos.Commands.Create;

public class CreateContactInfoTests : ContactInfoMockRepository
{
    private readonly CreateContactInfoCommandValidator _validator;
    private readonly CreateContactInfoCommand _command;
    private readonly CreateContactInfoCommand.CreateContactInfoCommandHandler _handler;

    public CreateContactInfoTests(IServiceProvider serviceProvider,
        ContactInfoFakeData fakeData,
        CreateContactInfoCommandValidator validator,
        CreateContactInfoCommand command)
        : base(serviceProvider, fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new CreateContactInfoCommand.CreateContactInfoCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public void ContactInfoTypeEmptyShouldReturnError()
    {
        _command.Type = string.Empty;

        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.FirstOrDefault(x => x.PropertyName == "Type" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator);

        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact]
    public async Task CreateShouldSuccessfully()
    {
        _command.Type = "Email";
        _command.Value = "test@test.com";
        _command.UserId = Guid.NewGuid();

        CustomResponseDto<CreatedContactInfoResponse> result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "test@test.com", result.Data.Value);
    }
}