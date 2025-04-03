namespace TimesheetApp.Application.WorkingHour.Queries.ListWorkingHours;

public static class ListWorkingHoursQueryMapper
{
    public static ListWorkingHoursQueryResponse ToResponse(this Domain.Models.WorkingHour workingHour)
    {
        return new ListWorkingHoursQueryResponse
        {
            Id = workingHour.Id,
            ClientId = workingHour.ClientId,
            ClientName = workingHour.Client?.Name ?? "Unknown",
            ProjectId = workingHour.ProjectId,
            ProjectName = workingHour.Project?.Name ?? "Unknown",
            CategoryId = workingHour.CategoryId,
            CategoryName = workingHour.Category?.Name ?? "Unknown",
            Description = workingHour.Description,
            Time = workingHour.Time,
            Overtime = workingHour.Overtime,
            EmployeeId = workingHour.EmployeeId,
            EmployeeName = workingHour.Employee?.Name ?? "Unknown",
            Date = workingHour.Date,
        };
    }
    
    public static List<ListWorkingHoursQueryResponse> ToResponseList(this List<Domain.Models.WorkingHour> workingHours)
    {
        return workingHours.Select(c => c.ToResponse()).ToList();
    }
}