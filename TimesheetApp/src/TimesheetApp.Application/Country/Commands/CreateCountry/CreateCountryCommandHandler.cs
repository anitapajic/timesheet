using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CountryRepository;

namespace TimesheetApp.Application.Country.Commands.CreateCountry;

public sealed class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CreateCountryCommandResponse>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCountryCommandResponse> Handle(CreateCountryCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _countryRepository.ExistsByNameAsync(command.Name);
        if (exists)
        {
            throw new EntityAlreadyExistsException("Country with name '" + command.Name + "' already exists.");
        }
        
        var domainCountry = command.ToDomain();
        var createdCountry = await _countryRepository.CreateAsync(domainCountry, cancellationToken);
        
        await _unitOfWork.Save(cancellationToken);

        return createdCountry.ToResponse();
    }
}