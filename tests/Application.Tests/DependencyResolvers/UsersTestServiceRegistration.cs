using Application.Tests.Mocks.FakeData;
using Microsoft.Extensions.DependencyInjection;
using webAPI.Application.Features.Users.Commands.Create;
using webAPI.Application.Features.Users.Commands.Delete;
using webAPI.Application.Features.Users.Commands.Update;
using webAPI.Application.Features.Users.Queries.GetById;
using webAPI.Application.Features.Users.Queries.GetList;

namespace Application.Tests.DependencyResolvers;

public static class UsersTestServiceRegistration
{
    public static void AddUsersServices(this IServiceCollection services)
    {
        services.AddTransient<UserFakeData>();
        services.AddTransient<CreateUserCommand>();
        services.AddTransient<UpdateUserCommand>();
        services.AddTransient<DeleteUserCommand>();
        services.AddTransient<GetByIdUserQuery>();
        services.AddTransient<GetListUserQuery>();
        services.AddSingleton<CreateUserCommandValidator>();
        services.AddSingleton<UpdateUserCommandValidator>();
    }
}