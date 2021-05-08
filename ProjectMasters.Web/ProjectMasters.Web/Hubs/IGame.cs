using System.Threading.Tasks;

namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;

    using ProjectMasters.Web.DTOs;

    public interface IGame
    {
        Task AssignPersonAsync(object obj);

        Task SetupClientStateAsync(IEnumerable<PersonDto> personDto);
        Task AttackPersonAsync(object obj);
        Task KillUnit(object obj);
    }
}