using MediatR;

namespace TimesheetApp.Application.Client.Queries.ListClients
{
    public sealed record ListClientsQuery : IRequest<List<ListClientsQueryResponse>>;
}