using Application.Tests.Mocks.FakeData;
using Core.Domain.Entities;
using Core.Test.Application.Repositories;
using webAPI.Application.Features.ContactInfos.Profiles;
using webAPI.Application.Features.ContactInfos.Rules;
using webAPI.Application.Services.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class ContactInfoMockRepository : BaseMockRepository<IContactInfoRepository, ContactInfo, Guid, MappingProfiles, ContactInfoBusinessRules, ContactInfoFakeData>
{
    public ContactInfoMockRepository(IServiceProvider serviceProvider, ContactInfoFakeData fakeData)
        : base(serviceProvider, fakeData) { }
}
