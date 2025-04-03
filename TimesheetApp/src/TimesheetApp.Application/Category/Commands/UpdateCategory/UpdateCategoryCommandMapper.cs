using AutoMapper;

namespace TimesheetApp.Application.Category.Commands.UpdateCategory;

public static class UpdateCategoryCommandMapper 
{
    public static Domain.Models.Category ToDomain(this UpdateCategoryCommand command)
    {
        return new Domain.Models.Category
        {
            Id = command.Id,
            Name = command.Name
        };
    }

    public static UpdateCategoryCommandResponse ToResponse(this Domain.Models.Category category)
    {
        return new UpdateCategoryCommandResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}