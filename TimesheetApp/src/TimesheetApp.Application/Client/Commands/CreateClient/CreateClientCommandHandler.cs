using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Extensions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.CountryRepository;

namespace TimesheetApp.Application.Client.Commands.CreateClient;

public sealed class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CreateClientCommandResponse>
{
    private readonly IClientRepository _clientRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(IClientRepository clientRepository, ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateClientCommandResponse> Handle(CreateClientCommand command,
        CancellationToken cancellationToken)
    {
        var domainClient = command.ToDomain();
        var existingCountry = await _countryRepository.GetByNameAsync(command.CountryName)
                              ?? throw new NoDataFoundException("Country not found");
        
        domainClient.Country = existingCountry.ToCountryOverview();
        
        var createdClient = await _clientRepository.CreateAsync(domainClient, cancellationToken);

        await _unitOfWork.Save(cancellationToken);
        return createdClient.ToResponse();
    }
}