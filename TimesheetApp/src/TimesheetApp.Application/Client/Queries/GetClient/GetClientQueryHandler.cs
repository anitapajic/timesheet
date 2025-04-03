using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.ClientRepository;

namespace TimesheetApp.Application.Client.Queries.GetClient;

public class GetClientQueryHandler : IRequestHandler<GetClientQuery, GetClientQueryResponse>
{
    private readonly IClientRepository _clientRepository;

    public GetClientQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task<GetClientQueryResponse> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.Get(request.Id, cancellationToken);
        if (client == null) throw new NoDataFoundException("Client not found");
        return client.ToResponse();
    }
}