using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Country.Commands.DeleteCountry;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CountryRepository;
using Xunit;

namespace UnitTest.Application.Country.Commands.DeleteCountry;

public class DeleteCountryCommandHandlerTest
{
    
    [Fact]
    public async Task Handle_ShouldDeleteCountry_WhenExists()
    {
        var command = new DeleteCountryCommand(Guid.NewGuid());
        var country = new TimesheetApp.Domain.Models.Country { Id = command.Id, Name = "TestCountry" };

        var testContext = new TestContext()
            .WithCountryRepositoryGetSetup(country)
            .WithCountryRepositoryDeleteAsyncSetup(country.Id);

        var handler = testContext.CreateHandler;
        
        await handler.Handle(command, CancellationToken.None);
        
        testContext.VerifyDeleteAsync(country.Id, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCountryNotFoundForDeletion()
    {
        var command = new DeleteCountryCommand(Guid.NewGuid());
        var testContext = new TestContext()
            .WithCountryRepositoryGetSetup(null!);
        
        var handler = testContext.CreateHandler;

        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(command, CancellationToken.None));
        
        testContext.VerifyDeleteAsync(command.Id, Times.Never);
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

        public TestContext WithCountryRepositoryGetSetup(TimesheetApp.Domain.Models.Country country)
        {
            _countryRepository.Setup(repo => repo.Get(
                    It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(country);
            return this;
        }

        public TestContext WithCountryRepositoryDeleteAsyncSetup(Guid id)
        {
            _countryRepository.Setup(repo => repo.DeleteAsync(
                id, It.IsAny<CancellationToken>()));
            return this;
        }

        public void VerifyDeleteAsync(Guid id, Func<Times> times)
        {
            _countryRepository.Verify(repo => repo.DeleteAsync(
                id,It.IsAny<CancellationToken>()), times);
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }
        
        public DeleteCountryCommandHandler CreateHandler => new(_countryRepository.Object, _unitOfWork.Object);
    }
    
}