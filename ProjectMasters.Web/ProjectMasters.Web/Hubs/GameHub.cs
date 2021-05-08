namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Linq;

    using Assets.BL;

    using Microsoft.AspNetCore.SignalR;

    using ProjectMasters.Games;
    using ProjectMasters.Web.DTOs;

    public class GameHub : Hub<IGame>
    {
        public void AssignPerson(int lineId, int personId)
        {
            var person = GameState._team.Persons.FirstOrDefault(p => p.Id == personId);
            var sendLine = GameState._project.Lines.FirstOrDefault(p => p.AssignedPersons.Contains(person));
            var line = GameState._project.Lines.FirstOrDefault(l => l.Id == lineId);

            if (sendLine == null || line == null)
                return;

            sendLine.AssignedPersons.Remove(person);
            line.AssignedPersons.Add(person);
            GameState.AssignPerson(line, person);
        }

        public void ChangeUnitPositionsServer(int lineId)
        {
            var lineToGetQueueIndecies = GameState._project.Lines.SingleOrDefault(x => x.Id == lineId);
            if (lineToGetQueueIndecies is null)
                // Не нашли линию проекта.
                // Это значит, что убили последнего монстра и линия была удалена.
                return;

            var unitPositionInfos = lineToGetQueueIndecies.Units.Select(x => new UnitDto(x)).ToList();
            Clients.Caller.ChangeUnitPositionsAsync(unitPositionInfos);
        }

        public void InitServerState()
        {
            if (GameState.Started)
            {
                var personDtos = GameState._team.Persons.Select(person => new PersonDto(person)
                {
                    // Получаем линию, которая содержит персонажа.
                    LineId = GameState._project.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(person))?.Id,
                }).ToArray();

                var unitDots = new List<UnitDto>();
                foreach (var line in GameState._project.Lines)
                {
                    foreach (var unit in line.Units)
                    {
                        var dto = new UnitDto(unit);
                        unitDots.Add(dto);
                    }
                }

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
                Player.ActiveDecisions = null;

            Player.WaitForDecision = Player.ActiveDecisions == null ? null : Player.ActiveDecisions[0];
        }
    }
}