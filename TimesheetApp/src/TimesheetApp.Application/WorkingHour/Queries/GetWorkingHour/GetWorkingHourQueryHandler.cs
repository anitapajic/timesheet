using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.WorkingHourRepository;

namespace TimesheetApp.Application.WorkingHour.Queries.GetWorkingHour;

public class GetWorkingHourQueryHandler: IRequestHandler<GetWorkingHourQuery, GetWorkingHourQueryResponse>
{
    private readonly IWorkingHourRepository _workingHourRepository;
    
    public GetWorkingHourQueryHandler(IWorkingHourRepository workingHourRepository)
    {
        _workingHourRepository = workingHourRepository;
    }
    
    public async Task<GetWorkingHourQueryResponse> Handle(GetWorkingHourQuery query,
        CancellationToken cancellationToken)
    {
        var workingHour = await _workingHourRepository.Get(query.Id, cancellationToken);
        if (workingHour == null) throw new NoDataFoundException("Working Hour not found");
        return workingHour.ToResponse();
    }
}