using FluentValidation;

namespace TimesheetApp.Application.Client.Commands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(60).NotNull();
        RuleFor(x => x.Address).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.City).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.PostalCode).MinimumLength(4).NotEmpty().MaximumLength(20);
        RuleFor(x => x.CountryName).MinimumLength(3).NotEmpty().MaximumLength(60);
    }
}