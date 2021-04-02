namespace Assets.BL
{
    public sealed class ErrorUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.Error;

        public override void ProcessCommit(Person person)
        {
            var isSuccessfullCommit = RollCommitSuccess(person.Skills);
            if (isSuccessfullCommit)
            {
                TimeLog += person.CommitPower;
                DoTakeDamage();
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.DeleteUnit(LineIndex, this);
                IsDead = true;
            }
        }
    }
}
