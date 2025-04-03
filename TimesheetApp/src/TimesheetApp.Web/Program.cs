using Timesheet.Infrastructure;
using Timesheet.Infrastructure.Context;
using TimesheetApp.Application;
using TimesheetApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<TimesheetAppContext>();
dataContext?.Database.EnsureCreated();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseErrorHandler();
app.MapControllers();
app.Run();