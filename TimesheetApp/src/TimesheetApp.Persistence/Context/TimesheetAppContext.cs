using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Timesheet.Infrastructure.Entities;
using TimesheetApp.Core.Repositories.Base;

namespace Timesheet.Infrastructure.Context;
public class TimesheetAppContext : DbContext, IUnitOfWork
{
    public TimesheetAppContext(DbContextOptions<TimesheetAppContext> options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<WorkingHour> WorkingHours { get; set; }
    
    public Task Save(CancellationToken cancellationToken)
    {
        return SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}