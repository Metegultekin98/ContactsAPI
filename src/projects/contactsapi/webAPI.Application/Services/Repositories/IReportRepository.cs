using Core.Domain.Entities;
using Core.Persistence.Repositories;

namespace webAPI.Application.Services.Repositories;

public interface IReportRepository : IAsyncRepository<Report, Guid>, IRepository<Report, Guid> { }