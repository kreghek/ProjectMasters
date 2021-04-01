using UnityEngine;

namespace Assets.BL
{
    public sealed class SubTaskUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.SubTask;

        public override void ProcessCommit(Person person)
        {
            var isSuccessfullCommit = RollCommitSuccess(person.Skills);
            if (isSuccessfullCommit)
            {
                TimeLog += person.CommitPower;
                DoTakeDamage();
            }

            if (Random.Range(1, 100) < person.ErrorChance)
            {
                var formation = ProjectUnitFormation.Instance;

                var errorUnit = CreateErrorUnit();

                if (Random.Range(1, 100) > 50)
                {
                    formation.AddUnitIntoLine(LineIndex, 0, errorUnit);
                }
                else
                {
                    formation.AddUnitIntoLine(LineIndex, QueueIndex + 1, errorUnit);
                }
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.DeleteUnit(LineIndex, this);
                IsDead = true;
            }
        }

        private ErrorUnit CreateErrorUnit()
        {
            var subTask = new ErrorUnit
            {
                Cost = Random.Range(4, 16),
                RequiredSkills = RequiredSkills
            };

            return subTask;
        }
    }
}
