using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Application.Country.Commands.CreateCountry;
using TimesheetApp.Application.Country.Commands.DeleteCountry;
using TimesheetApp.Application.Country.Commands.UpdateCountry;
using TimesheetApp.Application.Country.Queries.GetCountry;
using TimesheetApp.Application.Country.Queries.ListCountries;
using TimesheetApp.Controllers.Base;

namespace TimesheetApp.Controllers;

public class CountryController : BaseController
{
    private readonly IMediator _mediator;
    
    public CountryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ListCountriesQueryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ListCountriesQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryQueryResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCountryQuery(id), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateCountryCommandResponse>> Create(CreateCountryCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateCountryCommandResponse>> Update(Guid id, UpdateCountryCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCountryCommand(id), cancellationToken);
        return NoContent();
    }
    
    
}