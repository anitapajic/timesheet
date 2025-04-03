using FluentValidation;

namespace TimesheetApp.Application.Country.Commands.UpdateCountry;

public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(60).NotNull();
    }
}