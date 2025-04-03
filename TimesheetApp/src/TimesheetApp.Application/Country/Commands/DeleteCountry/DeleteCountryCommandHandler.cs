using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CountryRepository;

namespace TimesheetApp.Application.Country.Commands.DeleteCountry;

public sealed class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCountryCommand command, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.Get(command.Id, cancellationToken);
        if (country == null) throw new NoDataFoundException("Country not found");
        await _countryRepository.DeleteAsync(country.Id, cancellationToken);
        await _unitOfWork.Save(cancellationToken);

    }
}