using System.Linq.Expressions;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Services.ContactInfosService;

public class ContactInfoManager : IContactInfoService
{
    private readonly IContactInfoRepository _contactInfoRepository;

    public ContactInfoManager(IContactInfoRepository contactInfoRepository)
    {
        _contactInfoRepository = contactInfoRepository;
    }

    public async Task<ContactInfo?> GetAsync(
        Expression<Func<ContactInfo, bool>> predicate,
        Func<IQueryable<ContactInfo>, IIncludableQueryable<ContactInfo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContactInfo? contactInfo = await _contactInfoRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contactInfo;
    }

    public async Task<IPaginate<ContactInfo>?> GetListAsync(
        Expression<Func<ContactInfo, bool>>? predicate = null,
        Func<IQueryable<ContactInfo>, IOrderedQueryable<ContactInfo>>? orderBy = null,
        Func<IQueryable<ContactInfo>, IIncludableQueryable<ContactInfo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContactInfo> contactInfoList = await _contactInfoRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contactInfoList;
    }

    public async Task<ContactInfo> AddAsync(ContactInfo contactInfo)
    {
        ContactInfo addedContactInfo = await _contactInfoRepository.AddAsync(contactInfo);

        return addedContactInfo;
    }

    public async Task<ContactInfo> UpdateAsync(ContactInfo contactInfo)
    {
        ContactInfo updatedContactInfo = await _contactInfoRepository.UpdateAsync(contactInfo);

        return updatedContactInfo;
    }

    public async Task<ContactInfo> DeleteAsync(ContactInfo contactInfo, bool permanent = false)
    {
        ContactInfo deletedContactInfo = await _contactInfoRepository.DeleteAsync(contactInfo);

        return deletedContactInfo;
    }
}