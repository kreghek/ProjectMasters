using Microsoft.AspNetCore.SignalR;

using ProjectMasters.Games;

namespace ProjectMasters.Web.Hubs
{
    public class GameHub : Hub
    {

        public GameHub()
        {
            GameState.PersonAssigned += GameState_PersonAssigned;
        }

        private async void GameState_PersonAssigned(object sender, PersonAssignedEventArgs e)
        {
           // await Clients.All.SendAsync("assignPerson", new { PersonId = e.Person.Id, LineId = e.Line.Id });
        }
    }
}