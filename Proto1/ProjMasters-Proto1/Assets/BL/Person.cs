using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.BL
{
    public enum EffectType
    { 
        /// <summary>
        /// Decrease commit speed twice.
        /// </summary>
        Procrastination,

        /// <summary>
        /// Increase commit speed twice.
        /// </summary>
        Stream,

        /// <summary>
        /// Increase chance of error.
        /// </summary>
        Scattered
    }

    public class Effect
    { 
        public EffectType EffectType { get; set; }
        public float Lifetime { get; set; }
    }

    public class Person
    {
        private float _commitCounter;

        public string Name { get; set; }
        public float CommitSpeed { get; set; } = 1;
        public float ErrorChanceBase { get; set; } = 50;

        public Skill[] Skills { get; set; }

        public TraitType[] Traits { get; set; }

        public List<Effect> Effects { get; } = new List<Effect>();

        public event EventHandler<EventArgs> Commited;

        public int? LineIndex { get; set; }

        public void Update(ProjectUnitBase assignedUnit, float commitDeltaTime)
        {
            HandleTraits();

            HandleCurrentEffects();

            if (assignedUnit is null)
            {
                return;
            }

            ProgressUnitSolving(assignedUnit, commitDeltaTime);
        }

        private void HandleCurrentEffects()
        {
            throw new NotImplementedException();
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

                // Improve used skills

                var usedSkills = Skills.Where(x => unit.RequiredSkills.Contains(x.Scheme)).ToArray();
                foreach (var usedSkill in usedSkills)
                {
                    if (usedSkill.Level < 16)
                    {
                        usedSkill.Level += 0.01f;
                    }
                }
            }
        }
    }
}
