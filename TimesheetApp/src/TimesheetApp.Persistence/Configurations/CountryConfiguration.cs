using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasIndex(u => u.Name).IsUnique();
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(60);
    }
}