using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using webAPI.Application.Features.Companies.Commands.Create;
using Xunit;

namespace Application.Tests.Features.Companies.Commands.Create;

public class CreateCompanyTests : CompanyMockRepository
{
    private readonly CreateCompanyCommandValidator _validator;
    private readonly CreateCompanyCommand _command;
    private readonly CreateCompanyCommand.CreateCompanyCommandHandler _handler;

    public CreateCompanyTests(IServiceProvider serviceProvider,
        CompanyFakeData fakeData,
        CreateCompanyCommandValidator validator,
        CreateCompanyCommand command)
        : base(serviceProvider, fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new CreateCompanyCommand.CreateCompanyCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public void CompanyNameEmptyShouldReturnError()
    {
        _command.Name = string.Empty;

        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.FirstOrDefault(x => x.PropertyName == "Name" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator);

        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact]
    public async Task CreateShouldSuccessfully()
    {
        _command.Name = "First";
        _command.Address = "Last";
        _command.PhoneNumber = "+35987654321";

        CustomResponseDto<CreatedCompanyResponse> result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "First", result.Data.Name);
    }
}