using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Country.Commands.UpdateCountry;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CountryRepository;
using Xunit;

namespace UnitTest.Application.Country.Commands.UpdateCountry;

public class UpdateCountryCommandHandlerTest
{
    [Fact]
    public async Task Handle_ShouldUpdateCountry_WhenExists()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        var country = new TimesheetApp.Domain.Models.Country {Id = command.Id, Name = "OldCountry"};
        var updatedCountry = new TimesheetApp.Domain.Models.Country { Id = command.Id, Name = command.Name };

        testContext
            .WithCountryRepositoryGetSetup(command.Id, country)
            .WithCountryRepositoryUpdateAsyncSetUp(updatedCountry);
        
        var handler = testContext.CreateHandler;
        var result = await handler.Handle(command, CancellationToken.None);
   
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        
        testContext.VerifyUpdateAsync(Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_ThrowException_WhenCountryNotFound()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();

        testContext
            .WithCountryRepositoryGetSetup(command.Id, null);
        
        var handler = testContext.CreateHandler;
 
        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(command, CancellationToken.None));
            
        testContext.VerifyUpdateAsync(Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
   }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenCountryAlreadyExists()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        var country = new TimesheetApp.Domain.Models.Country {Id = command.Id, Name = "OldCountry"};
        testContext
            .WithCountryRepositoryGetSetup(command.Id, country)
            .WithCountryRepositoryExistNameSetUp(command.Name, true);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<EntityAlreadyExistsException>(() => handler.Handle(command, CancellationToken.None));
        
        testContext.VerifyUpdateAsync(Times.Never);
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

        public UpdateCountryCommand GetCommand()
        {
            return new UpdateCountryCommand { Id = Guid.NewGuid(), Name = "UpdatedCountry" };
        }
        
        public TestContext WithCountryRepositoryExistNameSetUp(string countryName, bool exists)
        {
            _countryRepository.Setup(repo => repo.ExistsByNameAsync(countryName))
                .ReturnsAsync(exists);
            
            return this;
        }
        
        public TestContext WithCountryRepositoryGetSetup(Guid id, TimesheetApp.Domain.Models.Country country)
        {
            _countryRepository.Setup(repo => repo.Get(
                    id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(country);
            return this;
        }
        
        public TestContext WithCountryRepositoryUpdateAsyncSetUp(TimesheetApp.Domain.Models.Country updatedCountry)
        {
            _countryRepository.Setup(repo => repo.UpdateAsync(
                    It.IsAny<TimesheetApp.Domain.Models.Country>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedCountry);

            return this;
        }
        
        public void VerifyUpdateAsync(Func<Times> times)
        {
            _countryRepository.Verify(repo => repo.UpdateAsync(
                It.IsAny<TimesheetApp.Domain.Models.Country>(), 
                It.IsAny<CancellationToken>()), times);
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }
        
        public UpdateCountryCommandHandler CreateHandler => new(_countryRepository.Object, _unitOfWork.Object);
    }
}