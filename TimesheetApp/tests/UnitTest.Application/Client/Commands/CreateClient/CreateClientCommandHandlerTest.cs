using Moq;
using TimesheetApp.Application.Client.Commands.CreateClient;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.CountryRepository;
using Xunit;

namespace UnitTest.Application.Client.Commands.CreateClient;

public class CreateClientCommandHandlerTest
{
    
    [Fact]
    public async Task Handle_ShouldCreateClient()
    {
        var testContext = new TestContext();
        
        var command = TestContext.GetCommand();
        var country = new TimesheetApp.Domain.Models.Country { Id = Guid.NewGuid(), Name = command.CountryName };

        testContext
            .WithClientRepositoryCreateAsyncSetUp()
            .WithCountryRepositoryGetByNameSetup(command.CountryName, country);
        
        var handler = testContext.CreateHandler;
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.CountryName, result.CountryName);
        
        testContext.VerifyCreateAsync(command.Name, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCountryNotFound()
    {
        var testContext = new TestContext();
        
        var command = TestContext.GetCommand();
        
        testContext
            .WithClientRepositoryCreateAsyncSetUp()
            .WithCountryRepositoryGetByNameSetup(command.CountryName, null!);
        
        var handler = testContext.CreateHandler;
        await Assert.ThrowsAsync<NoDataFoundException>(
            async () => await handler.Handle(command, CancellationToken.None));
        
        testContext.VerifyCreateAsync(command.Name, Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        
    }
    
    internal class TestContext
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<ICountryRepository> _countryRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public TestContext()
        {
            _clientRepository = new Mock<IClientRepository>();
            _countryRepository = new Mock<ICountryRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        public static CreateClientCommand GetCommand()
        {
            var command = new CreateClientCommand { Name = "Client", Address = "Adresa", City = "Grad", PostalCode = "10000"
                , CountryName = "Serbia123"};
            return command;
        }
        public TestContext WithClientRepositoryCreateAsyncSetUp()
        {
            _clientRepository.Setup(repo => repo.CreateAsync(
                    It.IsAny<TimesheetApp.Domain.Models.Client>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((TimesheetApp.Domain.Models.Client input, CancellationToken _) => input);
            
            return this;
        }
        
        public void VerifyCreateAsync(string clientName, Func<Times> times)
        {
            _clientRepository.Verify(repo => repo.CreateAsync(
                It.Is<TimesheetApp.Domain.Models.Client>(c => c.Name == clientName),
                It.IsAny<CancellationToken>()), times);
        }

        public TestContext WithCountryRepositoryGetByNameSetup(string name, TimesheetApp.Domain.Models.Country country)
        {
            _countryRepository.Setup(repo => repo.GetByNameAsync(
                name)).ReturnsAsync(country);
            return this;
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }

        public CreateClientCommandHandler CreateHandler => new(_clientRepository.Object, _countryRepository.Object, _unitOfWork.Object);
    }
}