using MediatR;
using TimesheetApp.Core.Repositories.CountryRepository;

namespace TimesheetApp.Application.Country.Queries.ListCountries;

public class ListCountriesQueryHandler : IRequestHandler<ListCountriesQuery, List<ListCountriesQueryResponse>>
{
    private readonly ICountryRepository _countryRepository;
    
    public ListCountriesQueryHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<List<ListCountriesQueryResponse>> Handle(ListCountriesQuery query, CancellationToken cancellationToken)
    {
        var countries = await _countryRepository.GetAll(cancellationToken);
        return countries.ToResponseList();
    }
}