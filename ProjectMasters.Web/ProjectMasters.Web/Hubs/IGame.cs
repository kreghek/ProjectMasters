﻿using System.Threading.Tasks;

namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Assets.BL;

    using ProjectMasters.Web.DTOs;

    public interface IGame
    {
        Task AssignPersonAsync(PersonDto person, LineDto line);

        Task SetupClientStateAsync(IEnumerable<PersonDto> personDto, IEnumerable<UnitDto> units);
        Task AttackPersonAsync(PersonDto person, UnitDto unit);
        Task KillUnitAsync(UnitDto unit);
        Task ChangeUnitPositionsAsync(IEnumerable<object> enumerable);
        Task CreateUnitAsync(UnitDto unit);
        Task AddEffectAsync(Effect effect);
    }
}