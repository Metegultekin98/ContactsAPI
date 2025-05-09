using Application.Tests.Mocks.FakeData;
using Microsoft.Extensions.DependencyInjection;
using webAPI.Application.Features.ContactInfos.Commands.Create;
using webAPI.Application.Features.ContactInfos.Commands.Delete;
using webAPI.Application.Features.ContactInfos.Commands.Update;
using webAPI.Application.Features.ContactInfos.Queries.GetById;
using webAPI.Application.Features.ContactInfos.Queries.GetList;
using webAPI.Application.Features.ContactInfos.Queries.GetListByDynamic;

namespace Application.Tests.DependencyResolvers;

public static class ContactInfoTestsServiceRegistration
{
    public static void AddContactInfosServices(this IServiceCollection services)
    {
        services.AddTransient<ContactInfoFakeData>();
        services.AddTransient<CreateContactInfoCommand>();
        services.AddTransient<UpdateContactInfoCommand>();
        services.AddTransient<DeleteContactInfoCommand>();
        services.AddTransient<GetByIdContactInfoQuery>();
        services.AddTransient<GetListContactInfoQuery>();
        services.AddTransient<GetListByDynamicContactInfoQuery>();
        services.AddSingleton<CreateContactInfoCommandValidator>();
        services.AddSingleton<UpdateContactInfoCommandValidator>();
    }
}