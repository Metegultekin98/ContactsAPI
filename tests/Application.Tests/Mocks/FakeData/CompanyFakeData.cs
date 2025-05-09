using Core.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class CompanyFakeData : BaseFakeData<Company, Guid>
{
    public override List<Company> CreateFakeData()
    {
        List<Company> data =
            new()
            {
                new Company
                {
                    Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802"),
                    Name = "Test Company1",
                    Address = "Test Address1",
                    PhoneNumber = "1234567890",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = Guid.Parse("00fba259-ad65-49ba-bb86-edc4bf6f27b6"),
                    Name = "Test Company2",
                    Address = "Test Address2",
                    PhoneNumber = "0987654321",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };
        return data;
    }
}