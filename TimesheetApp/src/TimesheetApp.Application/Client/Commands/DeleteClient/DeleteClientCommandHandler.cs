using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;

namespace TimesheetApp.Application.Client.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.Get(command.Id, cancellationToken);
        if (client == null) throw new NoDataFoundException("Client not found");
        await _clientRepository.DeleteAsync(client.Id, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
    }
}