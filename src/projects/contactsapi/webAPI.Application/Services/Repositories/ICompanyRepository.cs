using Core.Domain.Entities;
using Core.Persistence.Repositories;

namespace webAPI.Application.Services.Repositories;

public interface ICompanyRepository : IAsyncRepository<Company, Guid>, IRepository<Company, Guid> { }