namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Linq;

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
            // Это делаем, потому что фактически с клиента нам приходит индекс линии, а не Id. Индекс на 1 меньше, т.к. начинается с 0.
            // Внимание! Это может быть причиной бага.
            var correctLineId = lineId + 1;
            var lineToGetQueueIndecies = GameState._project.Lines.SingleOrDefault(x => x.Id == correctLineId);
            if (lineToGetQueueIndecies is null)
            {
                // Не нашли линию проекта.
                // Это значит, что убили последнего монстра и линия была удалена.
                return;
            }

            var unitPositionInfos = lineToGetQueueIndecies.Units.Select(x => new { UnitId = x.Id, QueueIndex = x.QueueIndex, LineId = lineId }).ToList();
            Clients.Caller.ChangeUnitPositionsAsync(unitPositionInfos);
        }
    }
}