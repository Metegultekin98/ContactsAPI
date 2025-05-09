using Application.Tests.Mocks.FakeData;
using Microsoft.Extensions.DependencyInjection;
using webAPI.Application.Features.Companies.Commands.Create;
using webAPI.Application.Features.Companies.Commands.Delete;
using webAPI.Application.Features.Companies.Commands.Update;
using webAPI.Application.Features.Companies.Queries.GetById;
using webAPI.Application.Features.Companies.Queries.GetList;

namespace Application.Tests.DependencyResolvers;

public static class CompanyTestsServiceRegistration
{
    public static void AddCompaniesServices(this IServiceCollection services)
    {
        services.AddTransient<CompanyFakeData>();
        services.AddTransient<CreateCompanyCommand>();
        services.AddTransient<UpdateCompanyCommand>();
        services.AddTransient<DeleteCompanyCommand>();
        services.AddTransient<GetByIdCompanyQuery>();
        services.AddTransient<GetListCompanyQuery>();
        services.AddSingleton<CreateCompanyCommandValidator>();
        services.AddSingleton<UpdateCompanyCommandValidator>();
    }
}