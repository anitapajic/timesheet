using FluentValidation.TestHelper;
using TimesheetApp.Application.Category.Commands.UpdateCategory;
using Xunit;

namespace UnitTest.Application.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandValidatorTest
{
    private readonly UpdateCategoryCommandValidator _validator;
    private readonly Guid _id;
    
    public UpdateCategoryCommandValidatorTest()
    {
        _validator = new UpdateCategoryCommandValidator();
        _id = Guid.NewGuid();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    public void Validator_ShouldHaveError_WhenNameIsInvalid(string name)
    {
        var command = new UpdateCategoryCommand{ Id = _id, Name = name };
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenNameIsTooLong()
    {
        var command = new UpdateCategoryCommand{ Id = _id, Name = new string('A', 61) };
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenNameIsValid()
    {
        var command = new UpdateCategoryCommand{ Id = _id, Name = "ValidName" };

        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
}