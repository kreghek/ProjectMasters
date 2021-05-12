namespace ProjectMasters.Web.Hubs
{
    using System.Linq;

    using Assets.BL;

    using DTOs;

    using Games;

    using Microsoft.AspNetCore.SignalR;

    public class GameHub : Hub<IGame>
    {
        public void AssignPersonToLineServer(GameState gameState, int lineId, int personId)
        {
            var person = gameState.Team.Persons.FirstOrDefault(p => p.Id == personId);
            var sendLine = gameState.Project.Lines.FirstOrDefault(p => p.AssignedPersons.Contains(person));
            var line = gameState.Project.Lines.FirstOrDefault(l => l.Id == lineId);

            if (line == null)
            {
                return;
            }

            sendLine?.AssignedPersons.Remove(person);
            line.AssignedPersons.Add(person);
            gameState.AssignPerson(line, person);
        }

        public void ChangeUnitPositionsServer(GameState gameState, int lineId)
        {
            var lineToGetQueueIndecies = gameState.Project.Lines.SingleOrDefault(x => x.Id == lineId);
            if (lineToGetQueueIndecies is null)
                // Не нашли линию проекта.
                // Это значит, что убили последнего монстра и линия была удалена.
            {
                return;
            }

            var unitPositionInfos = lineToGetQueueIndecies.Units.Select(x => new UnitDto(x)).ToList();
            Clients.Caller.ChangeUnitPositionsAsync(unitPositionInfos);
        }

        public void InitServerState(GameState gameState)
        {
            if (gameState.Started)
            {
                var personDtos = gameState.Team.Persons.Select(person => new PersonDto(person)
                {
                    // Получаем линию, которая содержит персонажа.
                    LineId = gameState.Project.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(person))?.Id
                }).ToArray();

                var unitDots = (from line in gameState.Project.Lines from unit in line.Units select new UnitDto(unit))
                    .ToList();

                Clients.Caller.SetupClientStateAsync(personDtos, unitDots);
            }
            else
            {
                gameState.Started = true;
            }
        }

        public void PreInitServerState(GameState gameState)
        {
            Clients.Caller.PreSetupClientAsync(gameState.Started);
        }

        public void SendDecision(GameState gameState, int number)
        {
            Player.WaitKeyDayReport = false;
            Player.WaitForDecision.Choises[number].Apply(gameState);

            Player.ActiveDecisions = Player.ActiveDecisions.Skip(1).ToArray();
            if (!Player.ActiveDecisions.Any())
            {
                Player.ActiveDecisions = null;
            }

            Player.WaitForDecision = Player.ActiveDecisions == null ? null : Player.ActiveDecisions[0];
        }
    }
}