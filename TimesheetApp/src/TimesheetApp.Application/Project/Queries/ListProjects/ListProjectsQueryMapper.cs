namespace TimesheetApp.Application.Project.Queries.ListProjects;

public static class ListProjectsQueryMapper
{
    public static ListProjectsQueryResponse ToResponse(this Domain.Models.Project project)
    {
        return new ListProjectsQueryResponse
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
    
    public static List<ListProjectsQueryResponse> ToResponseList(this List<Domain.Models.Project> projects)
    {
        return projects.Select(c => c.ToResponse()).ToList();
    }
}