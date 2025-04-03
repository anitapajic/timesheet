using System.Text.Json.Serialization;
using MediatR;

namespace TimesheetApp.Application.Category.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<UpdateCategoryCommandResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } 
}