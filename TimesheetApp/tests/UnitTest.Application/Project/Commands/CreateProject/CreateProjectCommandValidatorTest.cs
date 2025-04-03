using FluentValidation.TestHelper;
using TimesheetApp.Application.Project.Commands.CreateProject;
using Xunit;

namespace UnitTest.Application.Project.Commands.CreateProject;

public class CreateProjectCommandValidatorTest
{
    private readonly CreateProjectCommandValidator _validator;

    public CreateProjectCommandValidatorTest()
    {
        _validator = new CreateProjectCommandValidator();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    public void Validator_ShouldHaveError_WhenNameIsInvalid(string name)
    {
        var command = new CreateProjectCommand{Name = name};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenNameIsTooLong()
    {
        var command = new CreateProjectCommand{Name = new string('A', 61)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    public void Validator_ShouldHaveError_WhenDescriptionIsInvalid(string desc)
    {
        var command = new CreateProjectCommand{Description = desc};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Fact]
    public void Validator_ShouldHaveError_WhenDescriptionIsTooLong()
    {
        var command = new CreateProjectCommand{Description = new string('A', 101)};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenClientIdIsInvalid(Guid clientId)
    {
        var command = new CreateProjectCommand{ClientId = clientId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ClientId);
    }
    
    [Theory]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenLeadIdIsInvalid(Guid leadId)
    {
        var command = new CreateProjectCommand{LeadId = leadId};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LeadId);
    }
    
    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        var model = new CreateProjectCommand
        {
            Name = "name", 
            Description = "desc", 
            ClientId = Guid.NewGuid(), 
            LeadId = Guid.NewGuid(), 
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}