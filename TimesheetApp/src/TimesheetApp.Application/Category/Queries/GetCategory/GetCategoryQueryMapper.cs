namespace TimesheetApp.Application.Category.Queries.GetCategory;

public static class GetCategoryQueryMapper 
{
    public static GetCategoryQueryResponse ToResponse(this Domain.Models.Category category)
    {
        return new GetCategoryQueryResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}