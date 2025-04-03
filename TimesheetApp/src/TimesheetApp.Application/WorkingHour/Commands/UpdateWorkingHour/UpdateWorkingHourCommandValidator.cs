using FluentValidation;

namespace TimesheetApp.Application.WorkingHour.Commands.UpdateWorkingHour;

public class UpdateWorkingHourCommandValidator : AbstractValidator<UpdateWorkingHourCommand>
{
    public UpdateWorkingHourCommandValidator()
    {

        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.ClientId).NotEmpty().NotNull();
        RuleFor(x => x.ProjectId).NotEmpty().NotNull();
        RuleFor(x => x.CategoryId).NotEmpty().NotNull();
        RuleFor(x => x.EmployeeId).NotEmpty().NotNull();
        RuleFor(x => x.Time).NotEmpty().NotNull();
    }
}