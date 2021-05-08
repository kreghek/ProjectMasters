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

        public void ChangeUnitPositionsServer(int lineId)
        {
            var data = GameState._project.Lines.SingleOrDefault(x => x.Id == lineId)?.Units?.Select(x => new { UnitId = x.Id, QueueIndex = x.QueueIndex });
            Clients.Caller.ChangeUnitPositionsAsync(data);
        }
    }
}