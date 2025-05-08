using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using webAPI.Application.Features.Users.Constants;

namespace webAPI.Application.Features.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    public Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            throw new BusinessException(UsersMessages.UserDontExists);
        return Task.CompletedTask;
    }
}