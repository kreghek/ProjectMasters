namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using Assets.BL;

    using Microsoft.AspNetCore.SignalR;

    using ProjectMasters.Games;
    using ProjectMasters.Web.DTOs;

    public class GameHub : Hub<IGame>
    {
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

        public void ChangeUnitPositionsServer(int lineId)
        {
            var correctLineId = lineId;
            var lineToGetQueueIndecies = GameState._project.Lines.SingleOrDefault(x => x.Id == correctLineId);
            if (lineToGetQueueIndecies is null)
            {
                // Не нашли линию проекта.
                // Это значит, что убили последнего монстра и линия была удалена.
                return;
            }

            var unitPositionInfos = lineToGetQueueIndecies.Units.Select(x => new UnitDto(x)).ToList();
            Clients.Caller.ChangeUnitPositionsAsync(unitPositionInfos);
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