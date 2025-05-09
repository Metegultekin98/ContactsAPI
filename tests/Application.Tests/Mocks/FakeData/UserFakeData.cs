using Core.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class UserFakeData : BaseFakeData<User, Guid>
{
    public override List<User> CreateFakeData()
    {
        List<User> data =
            new()
            {
                new User
                {
                    Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802"),
                    FirstName = "Test",
                    LastName = "User1",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new User
                {
                    Id = Guid.Parse("00fba259-ad65-49ba-bb86-edc4bf6f27b6"),
                    FirstName = "Test",
                    LastName = "User2",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };
        return data;
    }
}