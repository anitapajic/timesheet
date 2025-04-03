using MediatR;

namespace TimesheetApp.Application.Client.Queries.GetClient

{
    public sealed record GetClientQuery(Guid Id) : IRequest<GetClientQueryResponse>;
}