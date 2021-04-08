using UnityEngine;

namespace Assets.BL
{
    public sealed class SubTaskUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.SubTask;

        public override void ProcessCommit(float commitPower, bool isCritical, Person person)
        {
            var isSuccessfullCommit = RollCommitSuccess(person.MasteryLevels);
            if (isSuccessfullCommit)
            {
                TimeLog += commitPower;
                DoTakeDamage(commitPower, isCritical);
            }

            if (Cost * 0.5f < TimeLog && Random.Range(1, 100) < person.ErrorChance)
            {
                var formation = ProjectUnitFormation.Instance;

                var errorCount = Random.Range(1, 5);
                for (int errorIndex = 0; errorIndex < errorCount; errorIndex++)
                {
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

                person.ErrorMadeCount += errorCount;
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.ResolveUnit(LineIndex, this);
                person.SubTasksCompleteCount++;
                IsDead = true;
                person.ProjectKnowedgeCoef += Person.PROJECT_KNOWEDGE_INCREMENT + Person.PROJECT_KNOWEDGE_INCREMENT * person.SkillUpSpeed;
            }
            else
            {
                if ((Cost - TimeLog) > 8)
                {
                    HandleSpeechs(Time.deltaTime);
                }
            }
        }

        private ErrorUnit CreateErrorUnit()
        {
            var subTask = new ErrorUnit
            {
                Cost = Random.Range(1, 9),
                RequiredSkills = RequiredSkills
            };

            return subTask;
        }
    }
}
