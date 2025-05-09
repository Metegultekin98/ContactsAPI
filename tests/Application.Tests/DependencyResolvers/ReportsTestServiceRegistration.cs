using Application.Tests.Mocks.FakeData;
using Microsoft.Extensions.DependencyInjection;
using webAPI.Application.Features.Reports.Queries.GetById;
using webAPI.Application.Features.Reports.Queries.GetList;

namespace Application.Tests.DependencyResolvers;

public static class ReportsTestServiceRegistration
{
    public static void AddReportsServices(this IServiceCollection services)
    {
        services.AddTransient<ReportFakeData>();
        services.AddTransient<GetByIdReportQuery>();
        services.AddTransient<GetListReportQuery>();
    }
}