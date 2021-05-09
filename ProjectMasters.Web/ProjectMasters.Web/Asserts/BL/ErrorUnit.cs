namespace Assets.BL
{
    using ProjectMasters.Games;

    public sealed class ErrorUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.Error;

        public override void ProcessCommit(float commitPower, bool isCritical, Person person)
        {
            var isSuccessfullCommit = RollCommitSuccess(person.MasteryLevels);
            if (isSuccessfullCommit)
            {
                TimeLog += commitPower;
                GameState.DoUnitTakenDamage(this);
                DoTakeDamage(commitPower, isCritical);
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.ResolveUnit(LineIndex, this);
                person.ErrorCompleteCount++;
                IsDead = true;
            }
        }
    }
}
