using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using webAPI.Application.Features.ContactInfos.Constants;

namespace webAPI.Application.Features.ContactInfos.Rules;

public class ContactInfoBusinessRules : BaseBusinessRules
{
    public Task ContactInfoShouldBeExistsWhenSelected(ContactInfo? contactInfo)
    {
        if (contactInfo == null)
            throw new BusinessException(ContactInfosMessages.ContactInfoDontExists);
        return Task.CompletedTask;
    }
}