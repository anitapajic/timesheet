using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timesheet.Infrastructure.Context;
using Timesheet.Infrastructure.Mappers;
using Timesheet.Infrastructure.Mappers.Impl;
using Timesheet.Infrastructure.Repository.CategoryRepository;
using Timesheet.Infrastructure.Repository.ClientRepository;
using Timesheet.Infrastructure.Repository.CountryRepository;
using Timesheet.Infrastructure.Repository.EmployeeRepository;
using Timesheet.Infrastructure.Repository.ProjectRepository;
using Timesheet.Infrastructure.Repository.WorkingHourRepository;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.CountryRepository;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Core.Repositories.WorkingHourRepository;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TimesheetAppContext>(options => options.UseNpgsql(connection).EnableSensitiveDataLogging());
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TimesheetAppContext>());
        
        services.AddScoped<ICustomMapper<Country, Entities.Country>, CountryMapper>();
        services.AddScoped<ICustomMapper<Category, Entities.Category>, CategoryMapper>();
        services.AddScoped<ICustomMapper<Client, Entities.Client>, ClientMapper>();
        services.AddScoped<ICustomMapper<Employee, Entities.Employee>, EmployeeMapper>();
        services.AddScoped<ICustomMapper<Project, Entities.Project>, ProjectMapper>();
        services.AddScoped<ICustomMapper<WorkingHour, Entities.WorkingHour>, WorkingHourMapper>();
        
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IWorkingHourRepository, WorkingHourRepository>();
    }
}