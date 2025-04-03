using MediatR;

namespace TimesheetApp.Application.Country.Queries.GetCountry
{
    public sealed record GetCountryQuery(Guid Id) : IRequest<GetCountryQueryResponse>;
}