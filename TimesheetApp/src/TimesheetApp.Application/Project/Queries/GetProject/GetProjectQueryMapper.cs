namespace TimesheetApp.Application.Project.Queries.GetProject;

public static class GetProjectQueryMapper
{
    public static GetProjectQueryResponse ToResponse(this Domain.Models.Project project)
    {
        return new GetProjectQueryResponse
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