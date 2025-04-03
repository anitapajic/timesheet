using System.ComponentModel.DataAnnotations;
using Timesheet.Infrastructure.Entities.Base;

namespace Timesheet.Infrastructure.Entities;

public class Country : BaseEntity
{
    public required string Name { get; set; }
}