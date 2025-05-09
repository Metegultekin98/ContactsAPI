using Application.Tests.Mocks.FakeData;
using Core.Domain.Entities;
using Core.Test.Application.Repositories;
using webAPI.Application.Features.Users.Profiles;
using webAPI.Application.Features.Users.Rules;
using webAPI.Application.Services.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class UserMockRepository : BaseMockRepository<IUserRepository, User, Guid, MappingProfiles, UserBusinessRules, UserFakeData>
{
    public UserMockRepository(IServiceProvider serviceProvider, UserFakeData fakeData)
        : base(serviceProvider, fakeData) { }
}
