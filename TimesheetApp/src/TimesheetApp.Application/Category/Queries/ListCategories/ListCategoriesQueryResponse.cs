namespace TimesheetApp.Application.Category.Queries.ListCategories;

public sealed record ListCategoriesQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}