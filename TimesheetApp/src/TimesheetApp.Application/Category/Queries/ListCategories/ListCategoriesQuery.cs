using MediatR;

namespace TimesheetApp.Application.Category.Queries.ListCategories

{
    public sealed record ListCategoriesQuery : IRequest<List<ListCategoriesQueryResponse>>;
}