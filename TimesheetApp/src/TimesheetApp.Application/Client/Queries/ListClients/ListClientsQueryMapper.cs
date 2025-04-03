namespace TimesheetApp.Application.Client.Queries.ListClients;

public static class ListClientsQueryMapper
{
    public static ListClientsQueryResponse ToResponse(this Domain.Models.Client client)
    {
        return new ListClientsQueryResponse
        {
            Id = client.Id,
            Name = client.Name,
            Address = client.Address,
            City = client.City,
            PostalCode = client.PostalCode,
            CountryName = client.Country?.Name ?? string.Empty
        };
    }
    
    public static List<ListClientsQueryResponse> ToResponseList(this List<Domain.Models.Client> clients)
    {
        return clients.Select(c => c.ToResponse()).ToList();
    }
}