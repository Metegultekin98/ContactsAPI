using Application.Tests.Mocks.FakeData;
using Core.Domain.Entities;
using Core.Test.Application.Repositories;
using webAPI.Application.Features.Reports.Profiles;
using webAPI.Application.Features.Reports.Rules;
using webAPI.Application.Services.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class ReportMockRepository : BaseMockRepository<IReportRepository, Report, Guid, MappingProfiles, ReportBusinessRules, ReportFakeData>
{
    public ReportMockRepository(IServiceProvider serviceProvider, ReportFakeData fakeData)
        : base(serviceProvider, fakeData) { }
}