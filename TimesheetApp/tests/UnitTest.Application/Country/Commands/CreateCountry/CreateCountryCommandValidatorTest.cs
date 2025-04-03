using FluentValidation.TestHelper;
using TimesheetApp.Application.Country.Commands.CreateCountry;
using Xunit;

namespace UnitTest.Application.Country.Commands.CreateCountry;

public class CreateCountryCommandValidatorTest
{
    private readonly CreateCountryCommandValidator _validator;

    public CreateCountryCommandValidatorTest()
    {
        _validator = new CreateCountryCommandValidator();
    }
    
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    [InlineData("AB")]
    public void Validator_ShouldHaveError_WhenNameIsInvalid(string name)
    {
        var command = new CreateCountryCommand(name);
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenNameIsTooLong()
    {
        var command = new CreateCountryCommand(new string('A', 61));
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenNameIsValid()
    {
        var command = new CreateCountryCommand("ValidCountry");

        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
}