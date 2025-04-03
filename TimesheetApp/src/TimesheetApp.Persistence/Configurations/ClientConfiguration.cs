using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(60);
        
        builder.Property(c => c.Address)
            .HasMaxLength(100);
        
        builder.Property(c => c.City)
            .HasMaxLength(50);
        
        builder.Property(c => c.PostalCode)
            .HasMaxLength(20);
        
        builder.Property(c => c.CountryId)
            .IsRequired(); 
        
        builder.HasOne(c => c.Country)
            .WithMany()
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
