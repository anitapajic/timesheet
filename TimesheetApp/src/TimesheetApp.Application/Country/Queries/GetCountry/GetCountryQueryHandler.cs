using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.CountryRepository;

namespace TimesheetApp.Application.Country.Queries.GetCountry;

public sealed class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, GetCountryQueryResponse>
{
    private readonly ICountryRepository _countryRepository;

    public GetCountryQueryHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<GetCountryQueryResponse> Handle(GetCountryQuery request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.Get(request.Id, cancellationToken);
        if (country == null) throw new NoDataFoundException("Country not found");
        return country.ToResponse();
    }
}