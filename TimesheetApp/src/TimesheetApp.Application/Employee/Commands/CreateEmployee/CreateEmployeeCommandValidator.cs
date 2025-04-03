using FluentValidation;

namespace TimesheetApp.Application.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(60).NotNull();
        RuleFor(x => x.Username).NotEmpty().MinimumLength(2).MaximumLength(20).NotNull();
        RuleFor(x => x.Email).NotEmpty().MinimumLength(5).MaximumLength(30).NotNull().EmailAddress();
        RuleFor(x => x.Password).MinimumLength(6).NotEmpty().MaximumLength(30).NotNull();
        RuleFor(x => x.HoursPerWeek).NotEmpty().GreaterThan(0);
    }
}