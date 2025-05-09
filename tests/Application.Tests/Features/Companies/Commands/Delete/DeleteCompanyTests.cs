using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Responses.Concrete;
using webAPI.Application.Features.Companies.Commands.Delete;
using Xunit;

namespace Application.Tests.Features.Companies.Commands.Delete;

public class DeleteCompanyTests : CompanyMockRepository
{
    private readonly DeleteCompanyCommand.DeleteCompanyCommandHandler _handler;
    private readonly DeleteCompanyCommand _command;

    public DeleteCompanyTests(IServiceProvider serviceProvider, CompanyFakeData fakeData, DeleteCompanyCommand command)
        : base(serviceProvider, fakeData)
    {
        _command = command;
        _handler = new DeleteCompanyCommand.DeleteCompanyCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task DeleteShouldSuccessfully()
    {
        _command.Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802");
        CustomResponseDto<DeletedCompanyResponse> result = await _handler.Handle(_command, CancellationToken.None);
        Assert.NotNull(result);
    }

}