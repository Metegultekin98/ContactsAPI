using FluentValidation;

namespace webAPI.Application.Features.ContactInfos.Commands.Update;

public class UpdateContactInfoCommandValidator : AbstractValidator<UpdateContactInfoCommand>
{
    public UpdateContactInfoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(c => c.Type).NotEmpty().MinimumLength(1);
        RuleFor(c => c.Value).NotEmpty().MinimumLength(1);
        RuleFor(c => c.UserId).NotEmpty().NotEqual(Guid.Empty);
    }
}