namespace TimesheetApp.Application.Client.Queries.GetClient;

public static class GetClientQueryMapper
{
    public static GetClientQueryResponse ToResponse(this Domain.Models.Client client)
    {
        return new GetClientQueryResponse
        {
            Id = client.Id,
            Name = client.Name,
            Address = client.Address,
            City = client.City,
            PostalCode = client.PostalCode,
            CountryName = client.Country?.Name ?? string.Empty
        };
    }
}