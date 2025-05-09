using Core.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class ContactInfoFakeData : BaseFakeData<ContactInfo, Guid>
{
    public override List<ContactInfo> CreateFakeData()
    {
        List<ContactInfo> data =
            new()
            {
                new ContactInfo
                {
                    Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802"),
                    Type = "Email",
                    Value = "test@test.com",
                    UserId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new ContactInfo
                {
                    Id = Guid.Parse("00fba259-ad65-49ba-bb86-edc4bf6f27b6"),
                    Type = "Phone",
                    Value = "1234567890",
                    UserId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };
        return data;
    }
}