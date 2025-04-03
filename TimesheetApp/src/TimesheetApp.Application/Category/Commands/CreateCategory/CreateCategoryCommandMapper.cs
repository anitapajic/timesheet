namespace TimesheetApp.Application.Category.Commands.CreateCategory;

public static class CreateCategoryCommandMapper
{
    public static Domain.Models.Category ToDomain(this CreateCategoryCommand command)
    {
        return new Domain.Models.Category
        {
            Name = command.Name
        };
    }

    public static CreateCategoryCommandResponse ToResponse(this Domain.Models.Category category)
    {
        return new CreateCategoryCommandResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}