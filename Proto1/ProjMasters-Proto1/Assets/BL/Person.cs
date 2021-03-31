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

        public int X { get; set; }

        public int Y { get; set; }

        public void Update(float commitDeltaTime)
        {
            if (Assigned is null)
            {
                return;
            }

            X = Assigned.X - 1;
            Y = Assigned.Y;

            if (Assigned != null)
            {
                SolveAssignedUnit(Assigned, commitDeltaTime);

                if (Assigned != null && Assigned.IsDead)
                {
                    Assigned = null;
                }
            }
        }

        private void SolveAssignedUnit(ProjectUnitBase unit, float commitDeltaTime)
        {
            CommitCounter += commitDeltaTime;

            const float baseCommitTimeSeconds = 4;
            var targetCommitCounter = baseCommitTimeSeconds * Speed;

            if (CommitCounter >= targetCommitCounter)
            {
                CommitCounter = 0;
                Commited?.Invoke(this, EventArgs.Empty);

                unit.ProcessCommit();
            }
        }
    }
}
