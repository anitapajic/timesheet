using System.ComponentModel.DataAnnotations.Schema;
using Timesheet.Infrastructure.Entities.Base;

namespace Timesheet.Infrastructure.Entities;

public class Client : BaseEntity
{
    public required string Name { get; set; }
    
    public string? Address { get; set; }
    
    public string? City { get; set; }
    
    public string? PostalCode { get; set; }
    
    public Guid CountryId { get; set; }
    
    [ForeignKey("CountryId")]
    public Country? Country { get; set; }
}