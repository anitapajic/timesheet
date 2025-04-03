using FluentValidation;

namespace TimesheetApp.Application.Country.Commands.CreateCountry;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(60).NotNull();
    }
}