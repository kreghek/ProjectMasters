using System.Threading.Tasks;

namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectMasters.Web.DTOs;

    public interface IGame
    {
        Task AssignPersonAsync(object obj);

        Task SetupClientStateAsync(IEnumerable<PersonDto> personDto, IEnumerable<LineDto> lines, IEnumerable<UnitDto> units);
        Task AttackPersonAsync(object obj);
        Task KillUnitAsync(object obj);
        Task ChangeUnitPositionsAsync(IEnumerable<object> enumerable);
    }
}