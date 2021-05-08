namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectMasters.Web.DTOs;

    public interface IGame
    {
        Task AssignPersonAsync(object obj);

        Task AttackPersonAsync(object obj);

        Task SetupClientStateAsync(IEnumerable<PersonDto> personDto);
    }
}