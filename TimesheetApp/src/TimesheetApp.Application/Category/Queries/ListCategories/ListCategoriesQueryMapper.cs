namespace TimesheetApp.Application.Category.Queries.ListCategories;

public static class ListCategoriesQueryMapper
{
    public static ListCategoriesQueryResponse ToResponse(this Domain.Models.Category category)
    {
        return new ListCategoriesQueryResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }
    
    public static List<ListCategoriesQueryResponse> ToResponseList(this List<Domain.Models.Category> categories)
    {
        return categories.Select(c => c.ToResponse()).ToList();
    }
}