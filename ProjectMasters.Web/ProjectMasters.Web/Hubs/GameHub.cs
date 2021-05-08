namespace ProjectMasters.Web.Hubs
{
    using System.Linq;

    using Microsoft.AspNetCore.SignalR;

    using ProjectMasters.Games;
    using ProjectMasters.Web.DTOs;

    public class GameHub : Hub<IGame>
    {
        public void InitServerState()
        {
            var personDtos = GameState._team.Persons.Select(person => new PersonDto
            {
                Id = person.Id
            });

            Clients.Caller.SetupClientStateAsync(personDtos);
        }
    }
}