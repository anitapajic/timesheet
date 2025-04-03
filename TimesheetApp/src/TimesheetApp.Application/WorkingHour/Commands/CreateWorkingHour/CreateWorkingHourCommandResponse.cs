namespace TimesheetApp.Application.WorkingHour.Commands.CreateWorkingHour;

public class CreateWorkingHourCommandResponse
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string ClientName { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string? Description { get; set; }
    public int Time { get; set; }
    public int? Overtime { get; set; }
    public Guid? EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public DateTime Date { get; set; }
}