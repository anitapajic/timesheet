using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(60);
        
        builder.Property(c => c.Username)
            .HasMaxLength(20);
        
        builder.Property(c => c.Email)
            .HasMaxLength(30);
        
        builder.Property(e => e.Role)
            .HasConversion<string>();
        
        builder.Property(e => e.EmployeeStatus)
            .HasConversion<string>();
        
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}