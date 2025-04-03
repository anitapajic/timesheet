using TimesheetApp.Application.Client.Queries.ListClients;
using Xunit;

namespace UnitTest.Application.Client.Queries.ListClients;

public class ListClientsQueryMapperTest
{
    [Fact]
    public void ToResponse_ShouldMapClientToResponse()
    {
        var client = new TimesheetApp.Domain.Models.Client
        {
            Id = Guid.NewGuid(),
            Name = "TestClient",
            Address = "Address",
            City = "City",
            PostalCode = "10000",
            Country = new TimesheetApp.Domain.Models.CountryOverview { Name = "TestCountry" }
        };
            
        var response = client.ToResponse();
            
        Assert.NotNull(response);
        Assert.Equal(client.Id, response.Id);
        Assert.Equal(client.Name, response.Name);
        Assert.Equal(client.Address, response.Address);
        Assert.Equal(client.City, response.City);
        Assert.Equal(client.PostalCode, response.PostalCode);
        Assert.Equal(client.Country.Name, response.CountryName);
    }

    [Fact]
    public void ToResponseList_ShouldMapListOfClientsToResponseList()
    {
        var clients = new List<TimesheetApp.Domain.Models.Client>
        {
            new() { Id = Guid.NewGuid(), Name = "TestClient", Address = "Address", City = "City", PostalCode = "10000", Country = new TimesheetApp.Domain.Models.CountryOverview { Name = "TestCountry" } },
            new() {  Id = Guid.NewGuid(), Name = "TestClient", Address = "Address", City = "City", PostalCode = "10000", Country = new TimesheetApp.Domain.Models.CountryOverview { Name = "TestCountry" } }
        };
            
        var responseList = clients.ToResponseList();
            
        Assert.NotNull(responseList);
        Assert.Equal(clients.Count, responseList.Count);
        Assert.Equal(clients[0].Id, responseList[0].Id);
        Assert.Equal(clients[0].Name, responseList[0].Name);
        Assert.Equal(clients[0].Address, responseList[0].Address);
        Assert.Equal(clients[0].City, responseList[0].City);
        Assert.Equal(clients[0].PostalCode, responseList[0].PostalCode);
        Assert.Equal(clients[0].Country.Name, responseList[0].CountryName);
        
        Assert.Equal(clients[1].Id, responseList[1].Id);
        Assert.Equal(clients[1].Name, responseList[1].Name);
        Assert.Equal(clients[1].Address, responseList[1].Address);
        Assert.Equal(clients[1].City, responseList[1].City);
        Assert.Equal(clients[1].PostalCode, responseList[1].PostalCode);
        Assert.Equal(clients[1].Country.Name, responseList[1].CountryName);
    }
}