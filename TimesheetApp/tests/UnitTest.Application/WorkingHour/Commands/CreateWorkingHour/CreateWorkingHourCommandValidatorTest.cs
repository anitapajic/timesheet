using FluentValidation.TestHelper;
using TimesheetApp.Application.WorkingHour.Commands.CreateWorkingHour;
using Xunit;

namespace UnitTest.Application.WorkingHour.Commands.CreateWorkingHour;

public class CreateWorkingHourCommandValidatorTest
{
    private readonly CreateWorkingHourCommandValidator _validator;

    public CreateWorkingHourCommandValidatorTest()
    {
        _validator = new CreateWorkingHourCommandValidator();
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenDescriptionIsEmpty()
    {
        var command = new CreateWorkingHourCommand{Description = ""};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenDescriptionIsTooLong()
    {
        var command = new CreateWorkingHourCommand{Description = new string('A', 1001)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenClientIdIsInvalid(Guid clientId)
    {
        var command = new CreateWorkingHourCommand{ClientId = clientId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ClientId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenProjectIdIsInvalid(Guid projectId)
    {
        var command = new CreateWorkingHourCommand{ProjectId = projectId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenCategoryIdIsInvalid(Guid categoryId)
    {
        var command = new CreateWorkingHourCommand{CategoryId = categoryId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CategoryId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenEmployeeIdIsInvalid(Guid employeeId)
    {
        var command = new CreateWorkingHourCommand{EmployeeId = employeeId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenTimeIsInvalid(int time)
    {
        var command = new CreateWorkingHourCommand{Time = time};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Time);
    }
    
    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        var model = new CreateWorkingHourCommand
        {
            Description = "desc", 
            ClientId = Guid.NewGuid(), 
            ProjectId = Guid.NewGuid(), 
            CategoryId = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            Time = 8
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
}