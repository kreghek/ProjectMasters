using System.Threading.Tasks;

namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;

    using Assets.BL;

    using ProjectMasters.Web.DTOs;

    public interface IGame
    {
        Task AssignPersonAsync(PersonDto person, LineDto line);

        Task SetupClientStateAsync(IEnumerable<PersonDto> personDto, IEnumerable<LineDto> lines, IEnumerable<UnitDto> units);
        Task AttackPersonAsync(PersonDto person, UnitDto unit);
        Task KillUnit(UnitDto unit);
        Task CreateUnitAsync(UnitDto unit);
        Task AddEffectAsync(Effect effect);
    }
}