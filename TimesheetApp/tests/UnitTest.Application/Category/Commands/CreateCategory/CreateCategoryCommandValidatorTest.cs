using FluentValidation.TestHelper;
using TimesheetApp.Application.Category.Commands.CreateCategory;
using Xunit;

namespace UnitTest.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidatorTest
{
    private readonly CreateCategoryCommandValidator _validator;

    public CreateCategoryCommandValidatorTest()
    {
        _validator = new CreateCategoryCommandValidator();
    }
    
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    public void Validator_ShouldHaveError_WhenNameIsInvalid(string name)
    {
        var command = new CreateCategoryCommand(name);
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenNameIsTooLong()
    {
        var command = new CreateCategoryCommand(new string('A', 61));
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenNameIsValid()
    {
        var command = new CreateCategoryCommand("ValidCategory");

        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
}