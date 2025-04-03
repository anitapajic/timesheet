using MediatR;

namespace TimesheetApp.Application.Category.Queries.GetCategory
{
    public sealed record GetCategoryQuery(Guid Id) : IRequest<GetCategoryQueryResponse>;
}