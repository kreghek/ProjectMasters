using System;

namespace Assets.BL
{
    public class Person
    {
        private float _commitCounter;

        public string Name { get; set; }
        public float CommitSpeed { get; set; } = 1;
        public float ErrorChanceBase { get; set; } = 50;

        public Skill[] Skills { get; set; }

        public TraitType[] Traits { get; set; }

        public event EventHandler<EventArgs> Commited;

        public int? LineIndex { get; set; }

        public void Update(ProjectUnitBase assignedUnit, float commitDeltaTime)
        {
            HandleTraits();

            if (assignedUnit is null)
            {
                return;
            }

            ProgressUnitSolving(assignedUnit, commitDeltaTime);
        }

        private void HandleTraits()
        {
            foreach (var trait in Traits)
            {
                switch (trait)
                {
                    case TraitType.CarefullDevelopment:
                        ErrorChanceBase = 50 - 25 / 2;
                        CommitSpeed = 2;
                        break;

                    case TraitType.RapidDevelopment:
                        ErrorChanceBase = 50 + 25 / 2;
                        CommitSpeed = 0.5f;
                        break;
                }
            }
        }

        private void ProgressUnitSolving(ProjectUnitBase unit, float commitDeltaTime)
        {
            _commitCounter += commitDeltaTime;

            const float baseCommitTimeSeconds = 2;
            var targetCommitCounter = baseCommitTimeSeconds * CommitSpeed;

            if (_commitCounter >= targetCommitCounter)
            {
                _commitCounter = 0;
                Commited?.Invoke(this, EventArgs.Empty);

                unit.ProcessCommit(this);
            }
        }
    }
}
