namespace ProjectMasters.Web.Hubs
{
    using System.Linq;

    using Assets.BL;

    using Microsoft.AspNetCore.SignalR;

    using ProjectMasters.Games;

    public class
        GameHub : Hub<IGame>
    {
        public void ChangeUnitPositionsServer(int lineId)
        {
            var data = GameState._project.Lines.SingleOrDefault(x => x.Id == lineId)?.Units
                ?.Select(x => new { UnitId = x.Id, x.QueueIndex });
            Clients.Caller.ChangeUnitPositionsAsync(data);
        }

        public void InitServerState()
        {
            GameState.Started = true;
        }

        public void SendDecision(int number)
        {
            Player.WaitKeyDayReport = false;
            Player.WaitForDecision.Choises[number].Apply();

            Player.ActiveDecisions = Player.ActiveDecisions.Skip(1).ToArray();
            if (!Player.ActiveDecisions.Any())
                Player.ActiveDecisions = null;

            Player.WaitForDecision = Player.ActiveDecisions == null ? null : Player.ActiveDecisions[0];
        }
    }
}