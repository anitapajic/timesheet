using MediatR;

namespace TimesheetApp.Application.Category.Commands.DeleteCategory

{
    public sealed record DeleteCategoryCommand(Guid Id) : IRequest;
}