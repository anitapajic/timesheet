using MediatR;

namespace TimesheetApp.Application.Country.Commands.UpdateCountry;

    public class UpdateCountryCommand : IRequest<UpdateCountryCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
    }
