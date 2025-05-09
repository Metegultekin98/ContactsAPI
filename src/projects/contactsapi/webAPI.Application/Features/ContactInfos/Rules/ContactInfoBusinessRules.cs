using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using webAPI.Application.Features.ContactInfos.Constants;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.ContactInfos.Rules;

public class ContactInfoBusinessRules : BaseBusinessRules
{
    private readonly IContactInfoRepository _contactInfoRepository;
    public ContactInfoBusinessRules(IContactInfoRepository contactInfoRepository)
    {
        _contactInfoRepository = contactInfoRepository;
    }
    public Task ContactInfoShouldBeExistsWhenSelected(ContactInfo? contactInfo)
    {
        if (contactInfo == null)
            throw new BusinessException(ContactInfosMessages.ContactInfoDontExists);
        return Task.CompletedTask;
    }
}