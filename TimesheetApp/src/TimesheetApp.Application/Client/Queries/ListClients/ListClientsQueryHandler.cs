using MediatR;
using TimesheetApp.Core.Repositories.ClientRepository;

namespace TimesheetApp.Application.Client.Queries.ListClients;

public class ListClientsQueryHandler : IRequestHandler<ListClientsQuery, List<ListClientsQueryResponse>>
{
    private readonly IClientRepository _clientRepository;

    public ListClientsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task<List<ListClientsQueryResponse>> Handle(ListClientsQuery query, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.GetAll(cancellationToken);
        return clients.ToResponseList();
    }
}