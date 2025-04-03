using MediatR;

namespace TimesheetApp.Application.Country.Commands.DeleteCountry
{
    public sealed record DeleteCountryCommand(Guid Id) : IRequest;
}