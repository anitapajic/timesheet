using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Country.Commands.CreateCountry;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CountryRepository;
using Xunit;

namespace UnitTest.Application.Country.Commands.CreateCountry;

public class CreateCountryCommandHandlerTest
{

    [Fact]
    public async Task Handle_ShouldCreateCountry()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        
        testContext
            .WithCountryRepositoryExistNameSetUp(command.Name, false)
            .WithCountryRepositoryCreateAsyncSetUp();
        
        var handler = testContext.CreateHandler;
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        
        testContext.VerifyCreateAsync(command.Name, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCountryAlreadyExists()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        
        testContext
            .WithCountryRepositoryExistNameSetUp(command.Name, true);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<EntityAlreadyExistsException>(() => handler.Handle(command, CancellationToken.None));
        
        testContext.VerifyCreateAsync(command.Name, Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
    }

    internal class TestContext
    {
        private readonly Mock<ICountryRepository> _countryRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public TestContext()
        {
            _countryRepository = new Mock<ICountryRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        public CreateCountryCommand GetCommand()
        {
            return new CreateCountryCommand("TestCountry");
        }

        public TestContext WithCountryRepositoryExistNameSetUp(string countryName, bool exists)
        {
            _countryRepository.Setup(repo => repo.ExistsByNameAsync(countryName))
                .ReturnsAsync(exists);
            
            return this;
        }
        
        public TestContext WithCountryRepositoryCreateAsyncSetUp()
        {
            _countryRepository.Setup(repo => repo.CreateAsync(It.IsAny<TimesheetApp.Domain.Models.Country>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((TimesheetApp.Domain.Models.Country input, CancellationToken _) => input);
            
            return this;
        }
        
        public void VerifyCreateAsync(string countryName, Func<Times> times)
        {
            _countryRepository.Verify(repo => repo.CreateAsync(
                It.Is<TimesheetApp.Domain.Models.Country>(c => c.Name == countryName),
                It.IsAny<CancellationToken>()), times);
        }

        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }

        public CreateCountryCommandHandler CreateHandler => new(_countryRepository.Object, _unitOfWork.Object);
    }
}