namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Assets.BL;

    using Games;

    using DTOs;

    public interface IGame
    {
        Task AddEffectAsync(Effect effect);
        Task AnimateUnitDamageAsync(UnitDto unitDto);
        Task AssignPersonAsync(PersonDto person, LineDto line);
        Task AttackPersonAsync(PersonDto person, UnitDto unit);
        Task ChangePersonLinePositionAsync(int lineId, int personId);
        Task ChangeUnitPositionsAsync(IEnumerable<UnitDto> unit);
        Task CreateUnitAsync(UnitDto unit);
        Task KillUnitAsync(UnitDto unit);
        Task PreSetupClientAsync(bool isGameStarted);
        Task RemoveEffectAsync(Effect effect);
        Task RemoveLineAsync(LineDto lineDto);
        Task RestPersonAsync(PersonDto person);
        Task SetStatusAsync(PlayerDto player);
        Task SetupClientStateAsync(IEnumerable<PersonDto> personDto, IEnumerable<UnitDto> units);
        Task SkillIsLearned(Skill skill);
        Task StartDecision(Decision decision);
        Task TirePersonAsync(PersonDto person);
    }
}