using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.WorkingHour.Commands.CreateWorkingHour;
using TimesheetApp.Application.WorkingHour.Commands.DeleteWorkingHour;
using TimesheetApp.Application.WorkingHour.Commands.UpdateWorkingHour;
using TimesheetApp.Application.WorkingHour.Queries.GetWorkingHour;
using TimesheetApp.Application.WorkingHour.Queries.ListWorkingHours;
using TimesheetApp.Controllers.Base;

namespace TimesheetApp.Controllers;

public class WorkingHourController : BaseController
{
    private readonly IMediator _mediator;
    
    public WorkingHourController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ListWorkingHoursQueryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ListWorkingHoursQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetWorkingHourQueryResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetWorkingHourQuery(id), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateWorkingHourCommandResponse>> Create(CreateWorkingHourCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateWorkingHourCommandResponse>> Update(Guid id, UpdateWorkingHourCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteWorkingHourCommand(id), cancellationToken);
        return NoContent();
    }
}