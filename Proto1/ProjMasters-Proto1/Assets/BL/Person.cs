using System;

namespace Assets.BL
{
    public class Person
    {
        private float _commitCounter;

        public string Name { get; set; }
        public float CommitSpeed { get; set; }

        public Skill[] Skills { get; set; }

        public event EventHandler<EventArgs> Commited;

        public int? LineIndex { get; set; }

        public void Update(ProjectUnitBase assignedUnit, float commitDeltaTime)
        {
            if (assignedUnit is null)
            {
                return;
            }

            SolveAssignedUnit(assignedUnit, commitDeltaTime);
        }

        private void SolveAssignedUnit(ProjectUnitBase unit, float commitDeltaTime)
        {
            _commitCounter += commitDeltaTime;

            const float baseCommitTimeSeconds = 2;
            var targetCommitCounter = baseCommitTimeSeconds * CommitSpeed;

            if (_commitCounter >= targetCommitCounter)
            {
                _commitCounter = 0;
                Commited?.Invoke(this, EventArgs.Empty);

                unit.ProcessCommit();
            }
        }
    }
}
