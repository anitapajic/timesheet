using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);
        
        builder.Property(e => e.ProjectStatus)
            .HasConversion<string>();

        builder.HasOne(p => p.Client)
            .WithMany()
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.Lead)
            .WithMany()
            .HasForeignKey(p => p.LeadId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(p => p.Employees)
            .WithMany(e => e.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "ProjectEmployee",
                j => j.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId"),
                j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId"));
    }
}