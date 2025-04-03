using MediatR;

namespace TimesheetApp.Application.Country.Queries.ListCountries

{
    public sealed record ListCountriesQuery : IRequest<List<ListCountriesQueryResponse>>;
}