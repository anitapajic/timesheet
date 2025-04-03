namespace TimesheetApp.Application.WorkingHour.Queries.GetWorkingHour;

public static class GetWorkingHourQueryMapper
{
    public static GetWorkingHourQueryResponse ToResponse(this Domain.Models.WorkingHour workingHour)
    {
        return new GetWorkingHourQueryResponse
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
}