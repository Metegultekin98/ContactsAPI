using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Core.Persistence.Repositories;
using webAPI.Application.Services.Repositories;

namespace webAPI.Persistence.Repositories;

public class ReportRepository : EfRepositoryBase<Report, Guid, BaseDbContext>, IReportRepository
{
    public ReportRepository(BaseDbContext context)
        : base(context) { }
}