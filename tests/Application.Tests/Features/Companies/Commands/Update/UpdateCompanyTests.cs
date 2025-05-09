using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using webAPI.Application.Features.Companies.Commands.Update;
using Xunit;

namespace Application.Tests.Features.Companies.Commands.Update;

public class UpdateCompanyTests : CompanyMockRepository
{
    private readonly UpdateCompanyCommandValidator _validator;
    private readonly UpdateCompanyCommand _command;
    private readonly UpdateCompanyCommand.UpdateCompanyCommandHandler _handler;

    public UpdateCompanyTests(IServiceProvider serviceProvider, CompanyFakeData fakeData, UpdateCompanyCommandValidator validator, UpdateCompanyCommand command)
        : base(serviceProvider, fakeData)
    {
        _validator = validator;
        _command = command;
        _handler = new UpdateCompanyCommand.UpdateCompanyCommandHandler(MockRepository.Object, Mapper, BusinessRules);
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
    public async Task UpdateShouldSuccessfully()
    {
        _command.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");
        _command.Name = "First";
        _command.Address = "Address";
        _command.PhoneNumber = "123456789";

        CustomResponseDto<UpdatedCompanyResponse> result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "First", result.Data.Name);
    }

}