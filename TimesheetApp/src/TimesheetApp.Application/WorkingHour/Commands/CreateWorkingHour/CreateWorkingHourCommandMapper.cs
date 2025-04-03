namespace TimesheetApp.Application.WorkingHour.Commands.CreateWorkingHour;

public static class CreateWorkingHourCommandMapper
{
    public static Domain.Models.WorkingHour ToDomain(this CreateWorkingHourCommand command)
    {
        return new Domain.Models.WorkingHour
        {
            ClientId = command.ClientId,
            ProjectId = command.ProjectId,
            CategoryId = command.CategoryId,
            Description = command.Description,
            Time = command.Time,
            Overtime = command.Overtime,
            EmployeeId = command.EmployeeId,
            Date = command.Date,
        };
    }

    public static CreateWorkingHourCommandResponse ToResponse(this Domain.Models.WorkingHour workingHour)
    {
        return new CreateWorkingHourCommandResponse
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