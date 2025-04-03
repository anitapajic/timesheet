using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Configurations;

public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHour>
{
    public void Configure(EntityTypeBuilder<WorkingHour> builder)
    {
        builder.Property(wh => wh.Description)
            .HasMaxLength(1000);

        builder.Property(wh => wh.Time)
            .IsRequired();

        builder.HasOne(wh => wh.Client)
            .WithMany()
            .HasForeignKey(wh => wh.ClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(wh => wh.Project)
            .WithMany()
            .HasForeignKey(wh => wh.ProjectId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(wh => wh.Category)
            .WithMany()
            .HasForeignKey(wh => wh.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(wh => wh.Employee)
            .WithMany()
            .HasForeignKey(wh => wh.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}