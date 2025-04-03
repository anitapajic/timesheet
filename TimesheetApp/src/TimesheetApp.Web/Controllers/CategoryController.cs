using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.Category.Commands.CreateCategory;
using TimesheetApp.Application.Category.Commands.DeleteCategory;
using TimesheetApp.Application.Category.Commands.UpdateCategory;
using TimesheetApp.Application.Category.Queries.GetCategory;
using TimesheetApp.Application.Category.Queries.ListCategories;
using TimesheetApp.Controllers.Base;

namespace TimesheetApp.Controllers;

public class CategoryController : BaseController
{
    private readonly IMediator _mediator;
    
    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ListCategoriesQueryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ListCategoriesQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCategoryQueryResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCategoryQuery(id), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateCategoryCommandResponse>> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateCategoryCommandResponse>> Update(Guid id, UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return NoContent();
    }
}