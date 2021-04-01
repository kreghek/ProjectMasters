﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.BL
{
    public abstract class ProjectUnitBase
    {
        public abstract ProjectUnitType Type { get; }
        public float Cost { get; set; }
        public float TimeLog { get; set; }
        public SkillScheme[] RequiredSkills { get; set; }

        public abstract void ProcessCommit(IEnumerable<Skill> skills);

        public bool IsDead { get; set; }

        public int LineIndex { get; set; }

        public int QueueIndex { get; set; }

        public event EventHandler<EventArgs> TakeDamage;

        protected void DoTakeDamage()
        {
            TakeDamage?.Invoke(this, EventArgs.Empty);
        }

        protected float GetSuccessCommitRoll(IEnumerable<Skill> personSkills)
        {
            var usedSkills = personSkills.Where(x => RequiredSkills.Contains(x.Scheme)).ToArray();
            if (usedSkills.Any())
            {
                return usedSkills.Average(x => x.Level);
            }
            else
            {
                return 0;
            }
        }

        protected bool RollCommitSuccess(IEnumerable<Skill> skills)
        {
            var effectiveCommitRoll = UnityEngine.Random.Range(1, 16);
            var successCommitRoll = GetSuccessCommitRoll(skills);

            var successfullCommit = effectiveCommitRoll <= successCommitRoll;
            return successfullCommit;
        }
    }
}
