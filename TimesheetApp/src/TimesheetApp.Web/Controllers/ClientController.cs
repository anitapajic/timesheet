using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.Client.Commands.CreateClient;
using TimesheetApp.Application.Client.Commands.DeleteClient;
using TimesheetApp.Application.Client.Commands.UpdateClient;
using TimesheetApp.Application.Client.Queries.GetClient;
using TimesheetApp.Application.Client.Queries.ListClients;
using TimesheetApp.Controllers.Base;

namespace TimesheetApp.Controllers;

public class ClientController : BaseController
{
    private readonly IMediator _mediator;
    
    public ClientController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ListClientsQueryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ListClientsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetClientQueryResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetClientQuery(id), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateClientCommandResponse>> Create(CreateClientCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateClientCommandResponse>> Update(Guid id, UpdateClientCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteClientCommand(id), cancellationToken);
        return NoContent();
    }
}