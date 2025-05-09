using FluentValidation;

namespace webAPI.Application.Features.Users.Commands.CreateUserUserByLocation;

public class CreateUserUserByLocationCommandValidator : AbstractValidator<CreateUserUserByLocationCommand>
{
    public CreateUserUserByLocationCommandValidator()
    {
        RuleFor(c => c.CreateReportDto.RequestedFor).NotEmpty();
    }
}