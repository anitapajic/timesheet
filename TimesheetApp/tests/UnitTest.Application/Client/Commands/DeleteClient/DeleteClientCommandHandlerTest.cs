using Moq;
using TimesheetApp.Application.Client.Commands.DeleteClient;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using Xunit;

namespace UnitTest.Application.Client.Commands.DeleteClient;

public class DeleteClientCommandHandlerTest
{
     [Fact]
    public async Task Handle_ShouldDeleteClient_WhenExists()
    {
        var id = Guid.NewGuid();
        var command = new DeleteClientCommand(id);
        var client = new TimesheetApp.Domain.Models.Client
        {
            Name = "Client", Address = "Address", City = "City", PostalCode = "10000"
        };
            
        var testContext = new TestContext()
            .WithClientRepositoryGetSetup(client)
            .WithClientRepositoryDeleteAsyncSetup(client.Id);

        var handler = testContext.CreateHandler;
        await handler.Handle(command, CancellationToken.None);
        
        testContext.VerifyDeleteAsync(client.Id, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
     }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenClientNotFoundForDeletion()
    {
        var id = Guid.NewGuid();
        var command = new DeleteClientCommand(id);
        
        var testContext = new TestContext()
            .WithClientRepositoryGetSetup(null!);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(command, CancellationToken.None));
            
        testContext.VerifyDeleteAsync(command.Id, Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        
    }
    
    internal class TestContext
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public TestContext()
        {
            _clientRepository = new Mock<IClientRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        public TestContext WithClientRepositoryGetSetup(TimesheetApp.Domain.Models.Client client)
        {
            _clientRepository.Setup(repo => repo.Get(
                    It.IsAny<Guid>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(client);
            return this;
        }

        public TestContext WithClientRepositoryDeleteAsyncSetup(Guid id)
        {
            _clientRepository.Setup(repo => repo.DeleteAsync(
                id, It.IsAny<CancellationToken>()));
            return this;
        }

        public void VerifyDeleteAsync(Guid id, Func<Times> times)
        {
            _clientRepository.Verify(repo => repo.DeleteAsync(
                id,It.IsAny<CancellationToken>()), times);
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }
        
        public DeleteClientCommandHandler CreateHandler => new(_clientRepository.Object, _unitOfWork.Object);
    }
}