namespace TimesheetApp.Application.Project.Commands.UpdateProject;

public static class UpdateProjectCommandMapper
{
    public static Domain.Models.Project ToDomain(this UpdateProjectCommand command)
    {
        return new Domain.Models.Project
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            ClientId = command.ClientId,
            LeadId = command.LeadId,
            ProjectStatus = command.ProjectStatus,
            EmployeeIds = command.EmployeeIds ?? [],
        };
    }

    public static UpdateProjectCommandResponse ToResponse(this Domain.Models.Project project)
    {
        return new UpdateProjectCommandResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            ClientId = project.ClientId,
            ClientName = project.Client?.Name ?? "Unknown",
            LeadId = project.LeadId,
            LeadName = project.Lead?.Name ?? "Unknown",
            ProjectStatus = project.ProjectStatus,
            Employees = project.Employees
        };
    }
}