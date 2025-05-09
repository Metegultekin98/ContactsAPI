using System.Linq.Expressions;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using webAPI.Application.Features.Reports.Dtos;

namespace webAPI.Application.Services.ReportsService;

public interface IReportService
{
    Task<Report?> GetAsync(
        Expression<Func<Report, bool>> predicate,
        Func<IQueryable<Report>, IIncludableQueryable<Report, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<Report>?> GetListAsync(
        Expression<Func<Report, bool>>? predicate = null,
        Func<IQueryable<Report>, IOrderedQueryable<Report>>? orderBy = null,
        Func<IQueryable<Report>, IIncludableQueryable<Report, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<Report> AddAsync(Report report);
    Task<Report> UpdateAsync(Report report);
    Task<Report> DeleteAsync(Report report, bool permanent = false);
}