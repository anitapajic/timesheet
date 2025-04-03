namespace TimesheetApp.Application.Project.Commands.CreateProject;

public static class CreateProjectCommandMapper
{
    public static Domain.Models.Project ToDomain(this CreateProjectCommand command)
    {
        return new Domain.Models.Project
        {
            Name = command.Name,
            Description = command.Description,
            ClientId = command.ClientId,
            LeadId = command.LeadId,
        };
    }

    public static CreateProjectCommandResponse ToResponse(this Domain.Models.Project project)
    {
        return new CreateProjectCommandResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            ClientId = project.ClientId,
            ClientName = project.Client.Name,
            LeadId = project.LeadId,
            LeadName = project.Lead.Name,
            ProjectStatus = project.ProjectStatus,
        };
    }
}