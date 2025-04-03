using NSubstitute;
using TimesheetApp.Application.Client.Queries.GetClient;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.ClientRepository;
using Xunit;

namespace UnitTest.Application.Client.Queries.GetClient;

public class GetClientQueryHandlerTest
{
    [Fact]
    public async Task Handle_ShouldReturnClient_WhenExists()
    {
        var id = Guid.NewGuid();
        var query = new GetClientQuery(id);
        var client = new TimesheetApp.Domain.Models.Client { Name = "Client", Address = "Adresa", City = "Grad", 
            PostalCode = "10000", Country  = new TimesheetApp.Domain.Models.CountryOverview { Name = "Serbia"}};
            
        var testContext = new TestContext()
            .WithClientRepositoryGetSetup(query.Id, client);
        
        var handler = testContext.CreateHandler;
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(client.Id, result.Id);
        Assert.Equal(client.Name, result.Name);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenClientNotFound()
    {
        var id = Guid.NewGuid();
        var query = new GetClientQuery(id);
        
        var testContext = new TestContext()
            .WithClientRepositoryGetSetup(query.Id, null);
        
        var handler = testContext.CreateHandler;

        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }
    
    internal class TestContext
    {
        private readonly IClientRepository _clientRepository;
        
        public TestContext()
        {
            _clientRepository = Substitute.For<IClientRepository>();
        }
        
        public TestContext WithClientRepositoryGetSetup(Guid id, TimesheetApp.Domain.Models.Client client)
        {
            _clientRepository.Get(
                    id, Arg.Any<CancellationToken>())
                .Returns(client);
            return this;
        }
        
        public GetClientQueryHandler CreateHandler => new(_clientRepository);
    }
}