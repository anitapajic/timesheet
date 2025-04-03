using FluentValidation;

namespace TimesheetApp.Application.Project.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(60).NotNull();
        RuleFor(x => x.Description).NotEmpty().MinimumLength(2).MaximumLength(100).NotNull();
        RuleFor(x => x.ClientId).NotEmpty().NotNull();
        RuleFor(x => x.LeadId).NotEmpty().NotNull();
      
    }
}