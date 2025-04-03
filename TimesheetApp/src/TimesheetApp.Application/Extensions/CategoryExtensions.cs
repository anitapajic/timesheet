using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Extensions;

public static class CategoryExtensions
{
    public static CategoryOverview ToCategoryOverview(this Domain.Models.Category category)
        => new CategoryOverview
        {
            Id = category.Id,
            Name = category.Name
        };
}