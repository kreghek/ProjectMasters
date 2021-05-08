namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.SignalR;

    using ProjectMasters.Games;
    using ProjectMasters.Web.DTOs;

    public class 
        GameHub : Hub<IGame>
    {
        public void InitServerState()
        {
            GameState.Started = true;

            
        }

        public void SendDecision(int number)
        {
            var count = number;
        }

        public void ChangeUnitPositionsServer(int lineId)
        {
            var data = GameState._project.Lines.SingleOrDefault(x => x.Id == lineId)?.Units?.Select(x => new { UnitId = x.Id, QueueIndex = x.QueueIndex });
            Clients.Caller.ChangeUnitPositionsAsync(data);
        }
    }
}