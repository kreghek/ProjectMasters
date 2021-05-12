namespace ProjectMasters.Web.Hubs
{
    using System.Linq;

    using Assets.BL;

    using DTOs;

    using Games;

    using Microsoft.AspNetCore.SignalR;

    public class GameHub : Hub<IGame>
    {
        public void AssignPersonToLineServer(int lineId, int personId)
        {
            var person = GameState.Team.Persons.FirstOrDefault(p => p.Id == personId);
            var sendLine = GameState.Project.Lines.FirstOrDefault(p => p.AssignedPersons.Contains(person));
            var line = GameState.Project.Lines.FirstOrDefault(l => l.Id == lineId);

            if (line == null)
            {
                return;
            }

            sendLine?.AssignedPersons.Remove(person);
            line.AssignedPersons.Add(person);
            GameState.AssignPerson(line, person);
        }

        public void ChangeUnitPositionsServer(int lineId)
        {
            var lineToGetQueueIndecies = GameState.Project.Lines.SingleOrDefault(x => x.Id == lineId);
            if (lineToGetQueueIndecies is null)
                // Не нашли линию проекта.
                // Это значит, что убили последнего монстра и линия была удалена.
            {
                return;
            }

            var unitPositionInfos = lineToGetQueueIndecies.Units.Select(x => new UnitDto(x)).ToList();
            Clients.Caller.ChangeUnitPositionsAsync(unitPositionInfos);
        }

        public void InitServerState()
        {
            if (GameState.Started)
            {
                var personDtos = GameState.Team.Persons.Select(person => new PersonDto(person)
                {
                    // Получаем линию, которая содержит персонажа.
                    LineId = GameState.Project.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(person))?.Id
                }).ToArray();

                var unitDots = (from line in GameState.Project.Lines from unit in line.Units select new UnitDto(unit))
                    .ToList();

                Clients.Caller.SetupClientStateAsync(personDtos, unitDots);
            }
            else
            {
                GameState.Started = true;
            }
        }

        public void PreInitServerState()
        {
            Clients.Caller.PreSetupClientAsync(GameState.Started);
        }

        public void SendDecision(int number)
        {
            Player.WaitKeyDayReport = false;
            Player.WaitForDecision.Choises[number].Apply();

            Player.ActiveDecisions = Player.ActiveDecisions.Skip(1).ToArray();
            if (!Player.ActiveDecisions.Any())
            {
                Player.ActiveDecisions = null;
            }

            Player.WaitForDecision = Player.ActiveDecisions == null ? null : Player.ActiveDecisions[0];
        }
    }
}