using FluentValidation.TestHelper;
using TimesheetApp.Application.Country.Commands.UpdateCountry;
using Xunit;

namespace UnitTest.Application.Country.Commands.UpdateCountry;

public class UpdateCountryCommandValidatorTest
{
    private readonly UpdateCountryCommandValidator _validator;
    private readonly Guid _id;
    
    public UpdateCountryCommandValidatorTest()
    {
        _validator = new UpdateCountryCommandValidator();
        _id = Guid.NewGuid();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    [InlineData("AB")]
    public void Validator_ShouldHaveError_WhenNameIsInvalid(string name)
    {
        var command = new UpdateCountryCommand{ Id = _id, Name = name };
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenNameIsTooLong()
    {
        var command = new UpdateCountryCommand{ Id = _id, Name = new string('A', 61) };
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenNameIsValid()
    {
        var command = new UpdateCountryCommand{ Id = _id, Name = "ValidName" };

        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
}