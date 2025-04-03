using MediatR;
using TimesheetApp.Core.Repositories.WorkingHourRepository;

namespace TimesheetApp.Application.WorkingHour.Queries.ListWorkingHours;

public class ListWorkingHoursQueryHandler : IRequestHandler<ListWorkingHoursQuery,  List<ListWorkingHoursQueryResponse>>
{
    private readonly IWorkingHourRepository _workingHourRepository;
        
    public ListWorkingHoursQueryHandler(IWorkingHourRepository workingHourRepository)
    {
        _workingHourRepository = workingHourRepository;
    }

    public async Task<List<ListWorkingHoursQueryResponse>> Handle(ListWorkingHoursQuery query, CancellationToken cancellationToken)
    {
        var workingHours = await _workingHourRepository.GetAll(cancellationToken);
        return workingHours.ToResponseList();
    }
}