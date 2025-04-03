using Moq;
using TimesheetApp.Application.Client.Commands.UpdateClient;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.CountryRepository;
using Xunit;

namespace UnitTest.Application.Client.Commands.UpdateClient;

public class UpdateClientCommandHandlerTest
{
    [Fact]
        public async Task Handle_ShouldUpdateClient()
        {
            var testContext = new TestContext();
            var command = testContext.GetCommand();
            var client = testContext.GetClient(command.Id);
            var country = new TimesheetApp.Domain.Models.Country { Name = command.CountryName };

            testContext
                .WithClientRepositoryGetSetup(command.Id, client)
                .WithCountryRepositoryGetByNameSetup(command.CountryName, country)
                .WithClientRepositoryUpdateAsyncSetUp();

            var handler = testContext.UpdateHandler;
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.CountryName, result.CountryName);

            testContext.VerifyUpdateAsync(command.Id, Times.Once);
            testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenClientNotFound()
        {
            var testContext = new TestContext();
            var command = testContext.GetCommand();

            testContext
                .WithClientRepositoryGetSetup(command.Id, null);

            var handler = testContext.UpdateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(
                async () => await handler.Handle(command, CancellationToken.None));
            
            testContext.VerifyUpdateAsync(command.Id, Times.Never);
            testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenCountryNotFound()
        {
            var testContext = new TestContext();
            var command = testContext.GetCommand();
            var client = testContext.GetClient(command.Id);

            testContext 
                .WithClientRepositoryGetSetup(command.Id, client)
                .WithCountryRepositoryGetByNameSetup(command.CountryName, null);

            var handler = testContext.UpdateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(
                async () => await handler.Handle(command, CancellationToken.None));
            
            testContext.VerifyUpdateAsync(command.Id, Times.Never);
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

        public UpdateClientCommand GetCommand()
        {
            var command = new UpdateClientCommand { Name = "Client", Address = "Adresa", City = "Grad", PostalCode = "10000"
                , CountryName = "Serbia123"};
            return command;
        }
        
        public TimesheetApp.Domain.Models.Client GetClient(Guid clientId)
        {
            var client = new TimesheetApp.Domain.Models.Client
            {
                Id = clientId,
                Name = "Old Client",
                Address = "Old Address",
                City = "Old City",
                PostalCode = "12345",
                Country = new TimesheetApp.Domain.Models.CountryOverview { Name = "Old Country" }
            };

            return client;
        }
        
        public TestContext WithClientRepositoryGetSetup(Guid id, TimesheetApp.Domain.Models.Client client)
        {
            _clientRepository.Setup(repo => repo.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(client);
            return this;
        }

        public TestContext WithClientRepositoryUpdateAsyncSetUp()
        {
            _clientRepository.Setup(repo => repo.UpdateAsync(
                    It.IsAny<TimesheetApp.Domain.Models.Client>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((TimesheetApp.Domain.Models.Client input, CancellationToken _) => input);
            return this;
        }

        public TestContext WithCountryRepositoryGetByNameSetup(string name, TimesheetApp.Domain.Models.Country country)
        {
            _countryRepository.Setup(repo => repo.GetByNameAsync(name))
                .ReturnsAsync(country);
            return this;
        }

        public void VerifyUpdateAsync(Guid clientId, Func<Times> times)
        {
            _clientRepository.Verify(repo => repo.UpdateAsync(
                It.Is<TimesheetApp.Domain.Models.Client>(c => c.Id == clientId),
                It.IsAny<CancellationToken>()), times);
        }
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }

        public UpdateClientCommandHandler UpdateHandler =>
            new(_clientRepository.Object, _countryRepository.Object, _unitOfWork.Object);
    }
}