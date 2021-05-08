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
            var personDtos = GameState._team.Persons.Select(person => new PersonDto
            {
                Id = person.Id
            });

            var lineDtos = GameState._project.Lines.Select(x=> new LineDto {
                Id = x.Id
            });

            var unitDots = new List<UnitDto>();
            foreach (var line in GameState._project.Lines)
            {
                foreach (var unit in line.Units)
                {
                    var dto = new UnitDto
                    {
                        Id = unit.Id,
                        LineId = line.Id,
                        Type = unit.Type.ToString(),
                        QueueIndex = unit.QueueIndex
                    };

                    unitDots.Add(dto);
                }
            }

            Clients.Caller.SetupClientStateAsync(personDtos, lineDtos, unitDots);
        }
    }
}