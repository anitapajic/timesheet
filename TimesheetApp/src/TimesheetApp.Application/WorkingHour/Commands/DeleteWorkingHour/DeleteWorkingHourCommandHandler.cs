using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.WorkingHourRepository;

namespace TimesheetApp.Application.WorkingHour.Commands.DeleteWorkingHour;

public class DeleteWorkingHourCommandHandler : IRequestHandler<DeleteWorkingHourCommand>
{
    private readonly IWorkingHourRepository _workingHourRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteWorkingHourCommandHandler(IWorkingHourRepository workingHourRepository, IUnitOfWork unitOfWork)
    {
        _workingHourRepository = workingHourRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteWorkingHourCommand command,
        CancellationToken cancellationToken)
    {
        var workingHour = await _workingHourRepository.Get(command.Id, cancellationToken);
        if (workingHour == null) throw new NoDataFoundException("Working Hour not found");
        await _workingHourRepository.DeleteAsync(workingHour.Id, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
    }
}