using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Extensions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.CountryRepository;

namespace TimesheetApp.Application.Client.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, UpdateClientCommandResponse>
{
    private readonly IClientRepository _clientRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(IClientRepository clientRepository, ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UpdateClientCommandResponse> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.Get(command.Id, cancellationToken)
                     ?? throw new NoDataFoundException("Client not found");
        
        client.Name = command.Name;
        client.Address = command.Address;
        client.City = command.City;
        client.PostalCode = command.PostalCode;
        var existingCountry = await _countryRepository.GetByNameAsync(command.CountryName)
                              ?? throw new NoDataFoundException("Country not found");
        
        client.Country = existingCountry.ToCountryOverview();
        

        var updatedClient = await _clientRepository.UpdateAsync(client, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
    
        return updatedClient.ToResponse();
    }
    
}