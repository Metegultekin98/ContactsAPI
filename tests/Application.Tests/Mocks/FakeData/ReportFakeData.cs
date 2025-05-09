using Core.Domain.ComplexTypes;
using Core.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class ReportFakeData : BaseFakeData<Report, Guid>
{
    public override List<Report> CreateFakeData()
    {
        List<Report> data =
            new()
            {
                new Report
                {
                    Id = Guid.Parse("a29b5b9d-6025-43d2-845a-c82db3b3a802"),
                    ReportStatu = Enums.ReportStatu.InProgress,
                    RequestedDate = DateTime.Now,
                    RequestedFor = "Test",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Report
                {
                    Id = Guid.Parse("00fba259-ad65-49ba-bb86-edc4bf6f27b6"),
                    ReportStatu = Enums.ReportStatu.Failed,
                    RequestedDate = DateTime.Now,
                    RequestedFor = "Test2",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };
        return data;
    }
}