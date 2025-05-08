using FluentValidation;

namespace webAPI.Application.Features.ContactInfos.Commands.Create;

public class CreateContactInfoCommandValidator : AbstractValidator<CreateContactInfoCommand>
{
    public CreateContactInfoCommandValidator()
    {
        RuleFor(c => c.Type).NotEmpty().MinimumLength(1);
        RuleFor(c => c.Value).NotEmpty().MinimumLength(1);
        RuleFor(c => c.UserId).NotEmpty().NotEqual(Guid.Empty);
    }
}