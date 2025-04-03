using FluentValidation.TestHelper;
using TimesheetApp.Application.WorkingHour.Commands.UpdateWorkingHour;
using Xunit;

namespace UnitTest.Application.WorkingHour.Commands.UpdateWorkingHour;

public class UpdateWorkingHourCommandValidatorTest
{
    private readonly UpdateWorkingHourCommandValidator _validator;

    public UpdateWorkingHourCommandValidatorTest()
    {
        _validator = new UpdateWorkingHourCommandValidator();
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenDescriptionIsEmpty()
    {
        var command = new UpdateWorkingHourCommand{Description = ""};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenDescriptionIsTooLong()
    {
        var command = new UpdateWorkingHourCommand{Description = new string('A', 1001)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenClientIdIsInvalid(Guid clientId)
    {
        var command = new UpdateWorkingHourCommand{ClientId = clientId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ClientId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenProjectIdIsInvalid(Guid projectId)
    {
        var command = new UpdateWorkingHourCommand{ProjectId = projectId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenCategoryIdIsInvalid(Guid categoryId)
    {
        var command = new UpdateWorkingHourCommand{CategoryId = categoryId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CategoryId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenEmployeeIdIsInvalid(Guid employeeId)
    {
        var command = new UpdateWorkingHourCommand{EmployeeId = employeeId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EmployeeId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenTimeIsInvalid(int time)
    {
        var command = new UpdateWorkingHourCommand{Time = time};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Time);
    }
    
    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        var model = new UpdateWorkingHourCommand
        {
            Id = Guid.NewGuid(),
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