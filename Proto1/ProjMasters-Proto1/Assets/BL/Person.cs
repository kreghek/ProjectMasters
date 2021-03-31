using System;
using System.Linq;

namespace Assets.BL
{
    public class Person
    {
        public string Name { get; set; }
        public float Speed { get; set; }

        public Skill[] Skills { get; set; }

        public ProjectUnitBase Assigned { get; set; }

        public float CommitCounter { get; set; }

        public event EventHandler<EventArgs> Commited;

        public void Update(float commitDeltaTime)
        {
            if (Assigned is null)
            {
                var foundUnit = FindFreeUnit();
                Assigned = foundUnit;
            }

            if (Assigned != null)
            {
                CommitCounter += commitDeltaTime;

                const float baseCommitTimeSeconds = 4;
                var targetCommitCounter = baseCommitTimeSeconds * Speed;

                if (CommitCounter >= targetCommitCounter)
                {
                    CommitCounter = 0;
                    Commited?.Invoke(this, EventArgs.Empty);

                    Assigned.ProcessCommit();

                    if (Assigned != null && Assigned.IsDead)
                    {
                        Assigned = null;
                    }
                }
            }
        }

        private ProjectUnitBase FindFreeUnit()
        {
            var matrix = ProjectUnitFormation.Instance.Matrix;

            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                for (var i = 0; i < matrix.GetLength(0); i++)
                {
                    var unit = matrix[i, j];

                    if (unit != null)
                    {
                        var assignedPersons = Team.Persons.Where(x => x.Assigned == unit);
                        if (!assignedPersons.Any())
                        {
                            var leftX = i - 1;
                            if (leftX < 0)
                            {
                                return unit;
                            }
                            else
                            {
                                var blockerUnit = matrix[leftX, j];
                                if (blockerUnit is null)
                                {
                                    return unit;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
