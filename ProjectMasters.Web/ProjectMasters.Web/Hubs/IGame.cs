namespace ProjectMasters.Web.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Assets.BL;

    using ProjectMasters.Web.DTOs;

    public interface IGame
    {
        Task AddEffectAsync(Effect effect);
        Task AssignPersonAsync(PersonDto person, LineDto line);
        Task AttackPersonAsync(PersonDto person, UnitDto unit);
        Task ChangeUnitPositionsAsync(IEnumerable<UnitDto> unit);
        Task CreateUnitAsync(UnitDto unit);
        Task KillUnitAsync(UnitDto unit);
        Task PreSetupClientAsync(bool isGameStarted);
        Task RemoveEffectAsync(Effect effect);
        Task RemoveLineAsync(LineDto lineDto);
        Task RestPersonAsync(PersonDto person);
        Task SetupClientStateAsync(IEnumerable<PersonDto> personDto, IEnumerable<UnitDto> units);
        Task StartDecision(Decision decision);
        Task TirePersonAsync(PersonDto person);
        Task SkillIsLearned(Skill skill);
    }
}