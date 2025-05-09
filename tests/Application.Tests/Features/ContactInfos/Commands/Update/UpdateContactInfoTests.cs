using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using webAPI.Application.Features.ContactInfos.Commands.Update;
using Xunit;

namespace Application.Tests.Features.ContactInfos.Commands.Update;

public class UpdateContactInfoTests : ContactInfoMockRepository
{
    private readonly UpdateContactInfoCommandValidator _validator;
    private readonly UpdateContactInfoCommand _command;
    private readonly UpdateContactInfoCommand.UpdateContactInfoCommandHandler _handler;

    public UpdateContactInfoTests(IServiceProvider serviceProvider, ContactInfoFakeData fakeData, UpdateContactInfoCommandValidator validator, UpdateContactInfoCommand command)
        : base(serviceProvider, fakeData)
    {
        _validator = validator;
        _command = command;
        _handler = new UpdateContactInfoCommand.UpdateContactInfoCommandHandler(MockRepository.Object, Mapper, BusinessRules);
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
    public async Task UpdateShouldSuccessfully()
    {
        _command.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");
        _command.Type = "Email";
        _command.Value = "test@test.com";
        _command.UserId = Guid.NewGuid();

        CustomResponseDto<UpdatedContactInfoResponse> result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "test@test.com", result.Data.Value);
    }

}