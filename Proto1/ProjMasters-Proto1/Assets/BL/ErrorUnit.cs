using UnityEngine;

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
                var commitPower = person.CommitPower;
                var isCritical = false;

                if (Random.Range(1, 100) < person.CritCommitChance)
                {
                    isCritical = true;
                }

                if (isCritical)
                {
                    commitPower *= person.CritCommitMultiplicator;
                }

                TimeLog += commitPower;
                DoTakeDamage(commitPower, isCritical);
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
