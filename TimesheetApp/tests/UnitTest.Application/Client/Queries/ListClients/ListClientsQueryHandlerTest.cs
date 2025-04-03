using NSubstitute;
using TimesheetApp.Application.Client.Queries.ListClients;
using TimesheetApp.Core.Repositories.ClientRepository;
using Xunit;

namespace UnitTest.Application.Client.Queries.ListClients;

public class ListClientsQueryHandlerTest
{
    private readonly IClientRepository _clientRepository;
    private readonly ListClientsQueryHandler _handler;
    
    public ListClientsQueryHandlerTest()
    {
        _clientRepository = Substitute.For<IClientRepository>();
        _handler = new ListClientsQueryHandler(_clientRepository);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnListOfClients()
    {
        var query = new ListClientsQuery();
        var clients = new List<TimesheetApp.Domain.Models.Client>
        {
            new() { Id = Guid.NewGuid(), Name = "TestClient", Address = "Address", City = "City", PostalCode = "10000", Country = new TimesheetApp.Domain.Models.CountryOverview { Name = "TestCountry" } },
            new() {  Id = Guid.NewGuid(), Name = "TestClient", Address = "Address", City = "City", PostalCode = "10000", Country = new TimesheetApp.Domain.Models.CountryOverview { Name = "TestCountry" } }
        };
            
        _clientRepository.GetAll(Arg.Any<CancellationToken>()).Returns(clients);
            
        var result = await _handler.Handle(query, CancellationToken.None);
            
        Assert.NotNull(result);
        Assert.Equal(clients.Count, result.Count);
        Assert.Equal(clients[0].Id, result[0].Id);
        Assert.Equal(clients[0].Name, result[0].Name);
        Assert.Equal(clients[1].Address, result[1].Address);
        Assert.Equal(clients[1].City, result[1].City);
        Assert.Equal(clients[1].PostalCode, result[1].PostalCode);
        Assert.Equal(clients[1].Country.Name, result[1].CountryName);
    }
}