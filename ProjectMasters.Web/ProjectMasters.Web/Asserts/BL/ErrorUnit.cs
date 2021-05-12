using ProjectMasters.Games;

namespace Assets.BL
{
    public sealed class ErrorUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.Error;

        public override void ProcessCommit(float commitPower, bool isCritical, Person person, GameState gameState)
        {
            var isSuccessfullCommit = RollCommitSuccess(person.MasteryLevels);
            if (isSuccessfullCommit)
            {
                TimeLog += commitPower;
                gameState.DoUnitTakenDamage(this);
                DoTakeDamage(commitPower, isCritical);
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.ResolveUnit(gameState, LineIndex, this);
                person.ErrorCompleteCount++;
                IsDead = true;
            }
        }
    }
}