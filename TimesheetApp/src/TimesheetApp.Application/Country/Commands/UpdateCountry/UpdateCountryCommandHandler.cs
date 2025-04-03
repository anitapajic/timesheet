using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CountryRepository;

namespace TimesheetApp.Application.Country.Commands.UpdateCountry;

public sealed class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, UpdateCountryCommandResponse>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateCountryCommandResponse> Handle(UpdateCountryCommand command, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.Get(command.Id, cancellationToken);
        if (country == null) throw new NoDataFoundException("Country not found");

        var exists = await _countryRepository.ExistsByNameAsync(command.Name);
        if (exists)
        {
            throw new EntityAlreadyExistsException("Country with name '" + command.Name + "' already exists.");
        }
        
        country.Name = command.Name;

        var updatedCountry = await _countryRepository.UpdateAsync(country, cancellationToken);
        
        await _unitOfWork.Save(cancellationToken);
        
        return updatedCountry.ToResponse();
    }
}