using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.Project.Commands.CreateProject;
using TimesheetApp.Application.Project.Commands.DeleteProject;
using TimesheetApp.Application.Project.Commands.UpdateProject;
using TimesheetApp.Application.Project.Queries.GetProject;
using TimesheetApp.Application.Project.Queries.ListProjects;
using TimesheetApp.Controllers.Base;

namespace TimesheetApp.Controllers;

public class ProjectController : BaseController
{
    private readonly IMediator _mediator;
    
    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ListProjectsQueryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ListProjectsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProjectQueryResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProjectQuery(id), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateProjectCommandResponse>> Create(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateProjectCommandResponse>> Update(Guid id, UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProjectCommand(id), cancellationToken);
        return NoContent();
    }
}