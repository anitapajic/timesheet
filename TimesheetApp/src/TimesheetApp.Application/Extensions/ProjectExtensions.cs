using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Extensions;

public static class ProjectExtensions
{
    public static ProjectOverview ToProjectOverview(this Domain.Models.Project project)
        => new ProjectOverview
        {
            Id = project.Id,
            Name = project.Name
        };
}