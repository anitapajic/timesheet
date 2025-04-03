using FluentValidation.TestHelper;
using TimesheetApp.Application.Employee.Commands.CreateEmployee;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandValidatorTest
{
    private readonly CreateEmployeeCommandValidator _validator;

    public CreateEmployeeCommandValidatorTest()
    {
        _validator = new CreateEmployeeCommandValidator();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    public void Validator_ShouldHaveError_WhenNameIsInvalid(string name)
    {
        var command = new CreateEmployeeCommand{Name = name};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenNameIsTooLong()
    {
        var command = new CreateEmployeeCommand{Name = new string('A', 61)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    public void Validator_ShouldHaveError_WhenUsernameIsInvalid(string username)
    {
        var command = new CreateEmployeeCommand{Username = username};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenUsernameIsTooLong()
    {
        var command = new CreateEmployeeCommand{Username = new string('A', 21)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("ABCDE")]
    public void Validator_ShouldHaveError_WhenPasswordIsInvalid(string password)
    {
        var command = new CreateEmployeeCommand{Password = password};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenPasswordIsTooLong()
    {
        var command = new CreateEmployeeCommand{Password = new string('A', 31)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
    
    [Theory]
    [InlineData(0)]
    public void Validator_ShouldHaveError_WhenHoursPerWeekIsInvalid(int hpw)
    {
        var command = new CreateEmployeeCommand{HoursPerWeek = hpw};
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
        var command = new CreateEmployeeCommand{Email = email};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenEmailIsTooLong()
    {
        var command = new CreateEmployeeCommand{Email = new string('A', 31)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        var model = new CreateEmployeeCommand
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