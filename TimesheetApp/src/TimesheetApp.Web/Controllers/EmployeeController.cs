using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.Employee.Commands.CreateEmployee;
using TimesheetApp.Application.Employee.Commands.DeleteEmployee;
using TimesheetApp.Application.Employee.Commands.UpdateEmployee;
using TimesheetApp.Application.Employee.Queries.GetEmployee;
using TimesheetApp.Application.Employee.Queries.ListEmployees;
using TimesheetApp.Controllers.Base;

namespace TimesheetApp.Controllers;

public class EmployeeController : BaseController
{
    private readonly IMediator _mediator;
    
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ListEmployeesQueryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ListEmployeesQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetEmployeeQueryResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeeQuery(id), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateEmployeeCommandResponse>> Create(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateEmployeeCommandResponse>> Update(Guid id, UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id), cancellationToken);
        return NoContent();
    }
}