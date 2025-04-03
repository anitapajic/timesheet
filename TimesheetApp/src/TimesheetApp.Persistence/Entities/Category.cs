using System.ComponentModel.DataAnnotations;
using Timesheet.Infrastructure.Entities.Base;

namespace Timesheet.Infrastructure.Entities;

public class Category : BaseEntity
{
    public required string Name { get; set; }
}