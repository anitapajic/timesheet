using MediatR;

namespace TimesheetApp.Application.WorkingHour.Commands.CreateWorkingHour;

public class CreateWorkingHourCommand : IRequest<CreateWorkingHourCommandResponse>
{
    public Guid ClientId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid CategoryId { get; set; }
    public string? Description { get; set; }
    public int Time { get; set; }
    public int? Overtime { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
}