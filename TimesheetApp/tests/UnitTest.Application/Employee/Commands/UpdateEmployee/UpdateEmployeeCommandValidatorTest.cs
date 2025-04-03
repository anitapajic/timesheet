using FluentValidation.TestHelper;
using TimesheetApp.Application.Employee.Commands.UpdateEmployee;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidatorTest
{
     private readonly UpdateEmployeeCommandValidator _validator;

    public UpdateEmployeeCommandValidatorTest()
    {
        _validator = new UpdateEmployeeCommandValidator();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    public void Validator_ShouldHaveError_WhenNameIsInvalid(string name)
    {
        var command = new UpdateEmployeeCommand{Name = name};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenNameIsTooLong()
    {
        var command = new UpdateEmployeeCommand{Name = new string('A', 61)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    public void Validator_ShouldHaveError_WhenUsernameIsInvalid(string username)
    {
        var command = new UpdateEmployeeCommand{Username = username};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenUsernameIsTooLong()
    {
        var command = new UpdateEmployeeCommand{Username = new string('A', 21)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("ABCDE")]
    public void Validator_ShouldHaveError_WhenPasswordIsInvalid(string password)
    {
        var command = new UpdateEmployeeCommand{Password = password};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenPasswordIsTooLong()
    {
        var command = new UpdateEmployeeCommand{Password = new string('A', 31)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
    
    [Theory]
    [InlineData(0)]
    public void Validator_ShouldHaveError_WhenHoursPerWeekIsInvalid(int hpw)
    {
        var command = new UpdateEmployeeCommand{HoursPerWeek = hpw};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.HoursPerWeek);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("ABCDE")]
    [InlineData("a.com")]
    public void Validator_ShouldHaveError_WhenEmailIsInvalid(string email)
    {
        var command = new UpdateEmployeeCommand{Email = email};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenEmailIsTooLong()
    {
        var command = new UpdateEmployeeCommand{Email = new string('A', 31)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        var model = new UpdateEmployeeCommand
        {
            Name = "name", 
            Username = "username", 
            Email = "email@mail.com", 
            Password = "123456", 
            HoursPerWeek = 40,
            Role = Role.Worker,
            EmployeeStatus = EmployeeStatus.Active
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}