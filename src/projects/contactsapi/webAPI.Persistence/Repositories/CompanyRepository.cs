using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Core.Persistence.Repositories;
using webAPI.Application.Services.Repositories;

namespace webAPI.Persistence.Repositories;

public class CompanyRepository : EfRepositoryBase<Company, Guid, BaseDbContext>, ICompanyRepository
{
    public CompanyRepository(BaseDbContext context)
        : base(context) { }
}