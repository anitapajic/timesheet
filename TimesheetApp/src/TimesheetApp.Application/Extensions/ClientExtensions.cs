using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Extensions;

public static class ClientExtensions
{
    public static ClientOverview ToClientOverview(this Domain.Models.Client client)
        => new ClientOverview
        {
            Id = client.Id,
            Name = client.Name
        };
}