using System.Linq.Expressions;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace webAPI.Application.Services.ContactInfosService;

public interface IContactInfoService
{
    Task<ContactInfo?> GetAsync(
        Expression<Func<ContactInfo, bool>> predicate,
        Func<IQueryable<ContactInfo>, IIncludableQueryable<ContactInfo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<ContactInfo>?> GetListAsync(
        Expression<Func<ContactInfo, bool>>? predicate = null,
        Func<IQueryable<ContactInfo>, IOrderedQueryable<ContactInfo>>? orderBy = null,
        Func<IQueryable<ContactInfo>, IIncludableQueryable<ContactInfo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<ContactInfo> AddAsync(ContactInfo contactInfo);
    Task<ContactInfo> UpdateAsync(ContactInfo contactInfo);
    Task<ContactInfo> DeleteAsync(ContactInfo contactInfo, bool permanent = false);
}