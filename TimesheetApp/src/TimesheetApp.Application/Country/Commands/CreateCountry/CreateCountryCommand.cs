using MediatR;

namespace TimesheetApp.Application.Country.Commands.CreateCountry

{
    public sealed record CreateCountryCommand(string Name) : IRequest<CreateCountryCommandResponse>;
}