using MediatR;

namespace TimesheetApp.Application.Category.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(string Name) : IRequest<CreateCategoryCommandResponse>;
}