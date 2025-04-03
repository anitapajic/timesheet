using FluentValidation.TestHelper;
using TimesheetApp.Application.Client.Commands.UpdateClient;
using Xunit;

namespace UnitTest.Application.Client.Commands.UpdateClient;

public class UpdateClientCommandValidatorTest
{
    private readonly UpdateClientCommandValidator _validator;

    public UpdateClientCommandValidatorTest()
    {
        _validator = new UpdateClientCommandValidator();
    }
    
    
     [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new UpdateClientCommand { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Short()
        {
            var model = new UpdateClientCommand { Name = "A" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Long()
        {
            var model = new UpdateClientCommand { Name = new string('A', 61) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var model = new UpdateClientCommand { Name = "Valid Name" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Address_Is_Empty()
        {
            var model = new UpdateClientCommand { Name = "Valid Name", Address = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Have_Error_When_City_Is_Empty()
        {
            var model = new UpdateClientCommand { Name = "Valid Name", City = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Have_Error_When_PostalCode_Is_Too_Short()
        {
            var model = new UpdateClientCommand { Name = "Valid Name", PostalCode = "123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Fact]
        public void Should_Have_Error_When_CountryName_Is_Too_Short()
        {
            var model = new UpdateClientCommand { Name = "Valid Name", CountryName = "AB" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CountryName);
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var model = new UpdateClientCommand
            {
                Name = "Valid Name",
                Address = "Valid Address",
                City = "Valid City",
                PostalCode = "12345",
                CountryName = "Valid Country"
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
}