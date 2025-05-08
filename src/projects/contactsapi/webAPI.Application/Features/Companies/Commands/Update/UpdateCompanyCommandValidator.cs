using FluentValidation;

namespace webAPI.Application.Features.Companies.Commands.Update;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        RuleFor(c => c.PhoneNumber).NotEmpty().MinimumLength(2);
    }
}